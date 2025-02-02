using Agama.Scripts.Entities;
using Agama.Scripts.Events;
using Scripts.EventChannel;
using Scripts.InvenSystem;
using Scripts.Items;
using Scripts.UI.Inven;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Player.Inven
{
    public class PlayerInvenData : InvenSystem.InvenData/*, IEntityComponent*/
    {
        [SerializeField] protected EventChannelSO _invenChannel;
        [SerializeField] private int _maxSlotCount;
        private Dictionary<EquipType, InventoryItem> _equipSlots;

        //private Player _player;
        #region Init Section
        public void Awake()//Initialize로 변경
        {
            //_player = entity as Player;
            inventory = new List<InventoryItem>();
            _equipSlots = new Dictionary<EquipType, InventoryItem>();
            _invenChannel.AddListener<InvenSwap>(InvenSwapHandler);
            _invenChannel.AddListener<InvenEquip>(InvenEquipHandler);
            _invenChannel.AddListener<RequestInvenData>(HandleRequestInventoryData);

            for (int i = 0; i < _maxSlotCount; i++)
                inventory.Add(null);
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
            _invenChannel.RemoveListener<RequestInvenData>(HandleRequestInventoryData);
            _invenChannel.RemoveListener<InvenSwap>(InvenSwapHandler);
            _invenChannel.AddListener<InvenEquip>(InvenEquipHandler);
        }
        #endregion
        private void HandleRequestInventoryData(RequestInvenData obj)
        {
            UpdateInventoryUI();
        }
        private void InvenSwapHandler(InvenSwap t)
        {
            if (t.isSame)
            {
                inventory[t.index2].AddStack(inventory[t.index1].stackSize); //꽉찼을때 판정 해줘야함
                inventory.Remove(inventory[t.index1]);
            }
            else
            {
                (inventory[t.index1], inventory[t.index2]) = (inventory[t.index2], inventory[t.index1]);
            }
            UpdateInventoryUI();
        }
        #region Equip Region
        private void InvenEquipHandler(InvenEquip t)
        {
            if (t.isUnEquip)
                UnEquip(t);
            else
                Equip(t);
        }

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

        private void UpdateInventoryUI()
        {
            var evt = InvenEvents.DataEvent;
            evt.items = inventory;
            evt.slotCount = _maxSlotCount;
            evt.equipments = _equipSlots;
            _invenChannel.InvokeEvent(evt);
        }
    }
}

