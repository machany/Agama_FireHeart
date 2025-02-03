using Agama.Scripts.Entities;
using Agama.Scripts.Events;
using Scripts.EventChannel;
using Scripts.InvenSystem;
using Scripts.Items;
using Scripts.UI.Inven;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Player.Inven
{
    public class PlayerInvenData : InvenSystem.InvenData/*, IEntityComponent*/
    {
        [SerializeField] protected EventChannelSO _invenChannel;
        [SerializeField] private int _maxSlotCount;//UI 갯수랑 맞춰야함
        [SerializeField] private int _quickSlotCount;//UI 갯수랑 맞춰야함
        private Dictionary<EquipType, InventoryItem> _equipSlots;
        public List<InventoryItem> quickSlots;

        //private Player _player;
        #region Init Section
        public void Awake()//Initialize로 변경
        {
            //_player = entity as Player;
            inventory = new List<InventoryItem>();
            _equipSlots = new Dictionary<EquipType, InventoryItem>();
            quickSlots = new List<InventoryItem>();

            _invenChannel.AddListener<InvenSwap>(InvenSwapHandler);
            _invenChannel.AddListener<InvenEquip>(InvenEquipHandler);
            _invenChannel.AddListener<RequestInvenData>(HandleRequestInventoryData);
            _invenChannel.AddListener<SetQuickSlot>(QuickSlotHandler);

            for (int i = 0; i < _maxSlotCount; i++)
                inventory.Add(null);
            for (int i = 0; i < _quickSlotCount; i++)
                quickSlots.Add(null);
        }



        private void Start()
        {
            UpdateInventoryUI();
        }

        public void AfterInit()
        {
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
                AddItem(inventory[0].data);
        }
        private void OnDestroy()
        {
            _invenChannel.RemoveListener<SetQuickSlot>(QuickSlotHandler);
            _invenChannel.RemoveListener<RequestInvenData>(HandleRequestInventoryData);
            _invenChannel.RemoveListener<InvenSwap>(InvenSwapHandler);
            _invenChannel.AddListener<InvenEquip>(InvenEquipHandler);
        }
        #endregion
        #region Handlers
        private void HandleRequestInventoryData(RequestInvenData obj)
        {
            UpdateInventoryUI();
        }
        private void InvenSwapHandler(InvenSwap t)
        {
            if (t.isSame)
            {
                AddItem(inventory[t.index2].data, inventory[t.index1].stackSize);
                inventory[t.index1] = null;
            }
            else
            {
                (inventory[t.index1], inventory[t.index2]) = (inventory[t.index2], inventory[t.index1]);
            }
            UpdateInventoryUI();
        }
        private void QuickSlotHandler(SetQuickSlot slot)
        {
            if (slot.isSwap)
                SwapQuickSlot(slot);
            else if (slot.isUnSet)
                UnSetQuickSlot(slot);
            else
                SetQuickSlot(slot);
        }

        private void InvenEquipHandler(InvenEquip t)
        {
            if (t.isUnEquip)
                UnEquip(t);
            else
                Equip(t);
        }
        #endregion
        private void SwapQuickSlot(SetQuickSlot slot)
        {
            (quickSlots[slot.quickSlotIndex], quickSlots[slot.quickSlotIndex2]) = (quickSlots[slot.quickSlotIndex2], quickSlots[slot.quickSlotIndex]);
            UpdateQuickSlot();
        }
        private void UnSetQuickSlot(SetQuickSlot t)
        {
            var quickSlot = quickSlots[t.quickSlotIndex];
            var slot = inventory[t.slotIndex];
            if (slot.data == null|| slot.data is UsableItemDataSO)
                if (t.isSame)
                {
                    AddItem(slot.data, quickSlot.stackSize);
                    quickSlots[t.quickSlotIndex] = null;
                }
                else
                {
                    (quickSlots[t.quickSlotIndex], inventory[t.slotIndex]) = (inventory[t.slotIndex], quickSlots[t.quickSlotIndex]);
                }
            UpdateInventoryUI(true);
        }

        private void SetQuickSlot(SetQuickSlot t)
        {
            var quickSlot = quickSlots[t.quickSlotIndex];
            var slot = inventory[t.slotIndex];
            if (slot.data is UsableItemDataSO)
                if (t.isSame)
                {
                    AddQuickSlotItem(quickSlot, slot.stackSize);
                    inventory[t.slotIndex] = null;
                }
                else
                {
                    (quickSlots[t.quickSlotIndex], inventory[t.slotIndex]) = (inventory[t.slotIndex], quickSlots[t.quickSlotIndex]);
                }
            UpdateInventoryUI(true);
        }

        #region Equip Region

        private void Equip(InvenEquip t)
        {
            InventoryItem item = inventory[t.index1];
            if (item.data is EquipItemDataSO dataSO)
            {
                var beforeArmor = _equipSlots.GetValueOrDefault(t.type);
                if (t.type == dataSO.equipType)
                {
                    if (beforeArmor != null)
                    {
                        _equipSlots[t.type] = item;
                        inventory[t.index1] = beforeArmor;
                        //EquipItemDataSO beforeEquipData = beforeEquipment.data as EquipItemDataSO;
                        //beforeEquipData?.RemoveModifier(_statCompo);
                    }
                    else
                    {
                        _equipSlots.Add(t.type, item);
                        inventory[t.index1] = null;
                    }
                    //equipItemData.AddModifier(_statCompo);
                }
            }
            UpdateInventoryUI();
            //else
            //{
            //    _itemSlots[t.index1].UpdateSlot(item);
            //}
        }

        private void UnEquip(InvenEquip t)
        {
            InventoryItem item = inventory[t.index1];
            if (item.data == null)
            {
                inventory[t.index1] = _equipSlots[t.type];
                _equipSlots.Remove(t.type);
            }
            else if (item.data is EquipItemDataSO dataSO)
            {
                if (t.type != dataSO.equipType)
                {
                    UpdateInventoryUI();
                    return;
                }
                inventory[t.index1] = _equipSlots[t.type];
                _equipSlots[t.type] = item;
            }
            UpdateInventoryUI();
        }
        #endregion
        #region Item Management
        public override void AddItem(ItemDataSO itemData, int count = 1)
        {
            IEnumerable<InventoryItem> items = GetItems(itemData);
            var canAddItem = items.FirstOrDefault(item => item.isFullStack == false);

            if (canAddItem == null)
            {
                CreateNewInventoryItem(itemData, count); //반드시 카운트가 스택 최대치보단 작게 드랍되어야 해.
            }
            else
            {
                int remain = canAddItem.AddStack(count);
                if (remain > 0)
                    CreateNewInventoryItem(itemData, remain);
            }

            UpdateInventoryUI();
        }
        public void AddQuickSlotItem(InventoryItem quickSlot, int count = 1)
        {
            int remain = quickSlot.AddStack(count);
            if (remain > 0)
                CreateNewInventoryItem(quickSlot.data, remain);
            UpdateInventoryUI(true);
        }
        private void CreateNewInventoryItem(ItemDataSO itemData, int count)
        {
            InventoryItem newItem = new InventoryItem(itemData, count);
            for (int i = 0; i < inventory.Count; i++)
                if (inventory[i].data == null)
                {
                    inventory[i] = newItem;
                    return;
                }
        }

        public override void RemoveItem(ItemDataSO itemData, int count)
        {
            UpdateInventoryUI();
        }

        public override bool CanAddItem(ItemDataSO itemData)
        {
            if (inventory.Count < _maxSlotCount - 1) return true;

            //만약 칸이 없다면 스택할 수 있는 아이템인지 찾아.
            IEnumerable<InventoryItem> items = GetItems(itemData);
            var canAddItem = items.FirstOrDefault(item => item.isFullStack == false);
            if (canAddItem == null) return false;

            return true;
        }
        #endregion

        private void UpdateInventoryUI(bool quickSlot = false)
        {
            var evt = InvenEvents.DataEvent;
            evt.items = inventory;
            evt.slotCount = _maxSlotCount;
            evt.equipments = _equipSlots;
            _invenChannel.InvokeEvent(evt);
            if (quickSlot)
                UpdateQuickSlot();
        }
        private void UpdateQuickSlot()
        {
            var evt = InvenEvents.QuickSlotDataEvent;
            evt.quickSlotItems = quickSlots;
            _invenChannel.InvokeEvent(evt);
        }
    }
}

