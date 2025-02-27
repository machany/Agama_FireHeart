using Agama.Scripts.Core;
using Agama.Scripts.Entities;
using Agama.Scripts.Events;
using Agama.Scripts.Players;
using Assets._0.Work.Dewmo123.Scripts.Items;
using Scripts.EventChannel;
using Scripts.Items;
using Scripts.UI.Inven;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Players.Inven
{
    //코드가 이꼬라지가 나기전에 고칠 방법이 있지 않았을까..?
    public class PlayerInvenData : InvenSystem.InvenData, IEntityComponent
    {
        [SerializeField] protected EventChannelSO _invenChannel;

        [SerializeField] private int _maxSlotCount;//UI 갯수랑 맞춰야함
        [SerializeField] private int _quickSlotCount;//UI 갯수랑 맞춰야함
        [SerializeField] private int _storageCount;//UI 갯수랑 맞춰야함
        private PlayerInputSO _input;//IEntityCompo 넣으면 거서 가꼬옴 지금은 임시

        private Dictionary<EquipType, InventoryItem> _equipSlots;
        public List<InventoryItem> quickSlots;
        public List<InventoryItem> storage;

        public int selectedSlotIndex;
        //선택됨 퀵슬롯 추가해야함
        public InventoryItem selectedItem => quickSlots[selectedSlotIndex];

        private Player _player;
        private EntityStat _statCompo;
        private GameObject _carringVisual;

        #region Init Section
        public void Initialize(Entity entity)//Initialize로 변경 완
        {
            _player = entity as Player;
            _statCompo = _player.GetComp<EntityStat>();
            inventory = new List<InventoryItem>();
            _equipSlots = new Dictionary<EquipType, InventoryItem>();
            quickSlots = new List<InventoryItem>();
            storage = new List<InventoryItem>();
            _input = _player.InputSO;

            _input.OnQuickSlotChangedEvent += ScrollWheelHandler;
            _player.OnUseItem += HandleUseItemSeleted;

            _invenChannel.AddListener<InvenSwap>(InvenSwapHandler);
            _invenChannel.AddListener<InvenEquip>(InvenEquipHandler);
            _invenChannel.AddListener<RequestInvenData>(HandleRequestInventoryData);
            _invenChannel.AddListener<SetQuickSlot>(QuickSlotHandler);
            _invenChannel.AddListener<SetStorageSlot>(StorageSlotHandler);
            _invenChannel.AddListener<CraftItem>(CraftItemHandler);
            _invenChannel.AddListener<RequestAddItem>(AcquireAddItem);

            for (int i = 0; i < _maxSlotCount; i++)
                inventory.Add(new InventoryItem(null, 0));
            for (int i = 0; i < _quickSlotCount; i++)
                quickSlots.Add(new InventoryItem(null, 0));
            for (int i = 0; i < _storageCount; i++)
                storage.Add(new InventoryItem(null, 0));
        }
        private void Start()
        {
            UpdateInventoryUI(true);
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
                AddItem(inventory[0].data);
        }

        private void OnDestroy()
        {
            _input.OnQuickSlotChangedEvent -= ScrollWheelHandler;
            _player.OnUseItem -= HandleUseItemSeleted;

            _invenChannel.RemoveListener<SetQuickSlot>(QuickSlotHandler);
            _invenChannel.RemoveListener<RequestInvenData>(HandleRequestInventoryData);
            _invenChannel.RemoveListener<SetStorageSlot>(StorageSlotHandler);
            _invenChannel.RemoveListener<InvenSwap>(InvenSwapHandler);
            _invenChannel.RemoveListener<InvenEquip>(InvenEquipHandler);
            _invenChannel.RemoveListener<CraftItem>(CraftItemHandler);
            _invenChannel.RemoveListener<RequestAddItem>(AcquireAddItem);

        }
        #endregion

        #region Handlers
        private void AcquireAddItem(RequestAddItem item)
        {
            AddItem(item.item, item.cnt);
        }
        private void ScrollWheelHandler(sbyte value)
        {
            selectedSlotIndex = value;

            ReloadQuickSlot();
        }
        public void ReloadQuickSlot()
        {
            if (_carringVisual != null)
                Destroy(_carringVisual);
            ItemDataSO itemSO = selectedItem != null ? selectedItem.data : null;
            if (itemSO == null)
                _player.ChangeQuickSlotItem(0, ItemDataSO.DEFAULT_DAMAGE);
            else
                _player.ChangeQuickSlotItem(itemSO.damageType, itemSO.attackDamage);
            if (itemSO is StructureItemDataSO structure)
            {
                _carringVisual = Instantiate(structure.structureIcon, _player.transform);
                _carringVisual.transform.position = _player.transform.position + structure.visualPivot;
            }
        }
        private void CraftItemHandler(CraftItem item)
        {
            CraftingRecipeSO recipe = item.recipe;
            if (recipe.MakeItem(this))
                AddItem(recipe.product);
            UpdateInventoryUI();
        }
        private void HandleRequestInventoryData(RequestInvenData obj)
        {
            UpdateInventoryUI();
        }

        private void HandleUseItemSeleted()
            => (selectedItem.data as IUsable)?.UseItem(_player);

        private void InvenSwapHandler(InvenSwap t)
        {
            if (t.isSame)
            {
                AddItem(inventory[t.index2].data, inventory[t.index1].stackSize);
                inventory[t.index1].Clear();
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
        private void StorageSlotHandler(SetStorageSlot slot)
        {
            if (slot.isSwap)
                SwapStorageSlot(slot);
            else if (slot.isUnSet)
                UnSetStorageSlot(slot);
            else
                SetStorageSlot(slot);
        }

        private void InvenEquipHandler(InvenEquip t)
        {
            if (t.isUnEquip)
                UnEquip(t);
            else
                Equip(t);
        }
        #endregion

        #region Set QuickSlot Region
        private void SwapQuickSlot(SetQuickSlot slot)
        {
            (quickSlots[slot.quickSlotIndex], quickSlots[slot.quickSlotIndex2]) = (quickSlots[slot.quickSlotIndex2], quickSlots[slot.quickSlotIndex]);
            UpdateQuickSlot();
        }
        private void UnSetQuickSlot(SetQuickSlot t)
        {
            var quickSlot = quickSlots[t.quickSlotIndex];
            var slot = inventory[t.slotIndex];
            if (slot.data == null || slot.data is IUsable)
                if (t.isSame)
                {
                    AddItem(slot.data, quickSlot.stackSize);
                    quickSlots[t.quickSlotIndex].Clear();
                }
                else
                {
                    (quickSlots[t.quickSlotIndex], inventory[t.slotIndex]) = (inventory[t.slotIndex], quickSlots[t.quickSlotIndex]);
                }
            UpdateInventoryUI();
        }

        private void SetQuickSlot(SetQuickSlot t)
        {
            var quickSlot = quickSlots[t.quickSlotIndex];
            var slot = inventory[t.slotIndex];
            if (slot.data is IUsable)
                if (t.isSame)
                {
                    AddQuickSlotItem(quickSlot, slot.stackSize);
                    inventory[t.slotIndex].Clear();
                }
                else
                {
                    (quickSlots[t.quickSlotIndex], inventory[t.slotIndex]) = (inventory[t.slotIndex], quickSlots[t.quickSlotIndex]);
                }
            UpdateInventoryUI();
        }

        #endregion

        #region Storage Region
        private void SwapStorageSlot(SetStorageSlot slot)
        {
            (storage[slot.storageSlotIndex], storage[slot.storageSlotIndex2]) = (storage[slot.storageSlotIndex2], storage[slot.storageSlotIndex]);
            UpdateStorage();
        }
        private void UnSetStorageSlot(SetStorageSlot t)
        {
            var storageSlot = storage[t.storageSlotIndex];
            var slot = inventory[t.slotIndex];
            if (slot.data == null || slot.data is IUsable)
                if (t.isSame)
                {
                    AddItem(slot.data, storageSlot.stackSize);
                    storage[t.storageSlotIndex].Clear();
                }
                else
                {
                    (storage[t.storageSlotIndex], inventory[t.slotIndex]) = (inventory[t.slotIndex], storage[t.storageSlotIndex]);
                }
            UpdateInventoryUI(true);
        }

        private void SetStorageSlot(SetStorageSlot t)
        {
            var storageSlot = storage[t.storageSlotIndex];
            var slot = inventory[t.slotIndex];
            if (slot.data)
                if (t.isSame)
                {
                    AddQuickSlotItem(storageSlot, slot.stackSize);
                    inventory[t.slotIndex].Clear();
                }
                else
                {
                    (storage[t.storageSlotIndex], inventory[t.slotIndex]) = (inventory[t.slotIndex], storage[t.storageSlotIndex]);
                }
            UpdateInventoryUI(true);
        }

        #endregion

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
                        EquipItemDataSO beforeEquipData = beforeArmor.data as EquipItemDataSO;
                        beforeEquipData?.RemoveModifier(_statCompo);
                    }
                    else
                    {
                        _equipSlots.Add(t.type, item.Clone() as InventoryItem);
                        inventory[t.index1].Clear();
                    }
                    dataSO.AddModifier(_statCompo);
                }
            }
            UpdateInventoryUI();
        }

        private void UnEquip(InvenEquip t)
        {
            InventoryItem item = inventory[t.index1];
            if (item.data == null)
            {
                inventory[t.index1] = _equipSlots[t.type];
                var equip = _equipSlots[t.type].data as EquipItemDataSO;
                equip.RemoveModifier(_statCompo);
                _equipSlots.Remove(t.type);
            }
            else if (item.data is EquipItemDataSO dataSO)
            {
                if (t.type != dataSO.equipType)
                {
                    UpdateInventoryUI();
                    return;
                }
                var before = _equipSlots[t.type].data as EquipItemDataSO;
                before.RemoveModifier(_statCompo);
                dataSO.AddModifier(_statCompo);
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
            var items = GetItems(itemData);
            if (count > GetAllItemStack(itemData))
                return;
            int remain = count;
            foreach (var item in items)
                if (remain > 0)
                    remain = item.RemoveStack(remain);
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
        public override InventoryItem GetItem(ItemDataSO itemData)
        {
            InventoryItem item = quickSlots.FirstOrDefault(inventoryItem => inventoryItem.data == itemData);
            if (item == null)
                item = base.GetItem(itemData);
            return item;
        }
        public override IEnumerable<InventoryItem> GetItems(ItemDataSO itemData)
        {
            var items = base.GetItems(itemData).ToList();
            var quickSlotItems = quickSlots.Where(item => item.data == itemData).ToList();
            quickSlotItems.ForEach(item => items.Add(item));
            return items;
        }
        #endregion

        #region Update

        private void UpdateInventoryUI(bool onStorage = false)
        {
            inventory.FindAll(item => item.data != null && item.stackSize == 0).ForEach(item => item.data = null);
            quickSlots.FindAll(item => item.data != null && item.stackSize == 0).ForEach(item => item.data = null);
            UpdateInvenSlot();
            UpdateQuickSlot();
            UpdateEquipSlot();
            if (onStorage)
                UpdateStorage();
        }

        private void UpdateInvenSlot()
        {
            var evt = InvenEvents.DataEvent;
            evt.items = inventory;
            _invenChannel.InvokeEvent(evt);
        }

        private void UpdateEquipSlot()
        {
            var evt = InvenEvents.EquipDataEvent;
            evt.equipments = _equipSlots;
            _invenChannel.InvokeEvent(evt);
        }
        private void UpdateQuickSlot()
        {
            ReloadQuickSlot();
            var evt = InvenEvents.QuickSlotDataEvent;
            evt.quickSlotItems = quickSlots;
            _invenChannel.InvokeEvent(evt);
        }
        private void UpdateStorage()
        {
            var evt = InvenEvents.StorageDataEvent;
            evt.storage = storage;
            _invenChannel.InvokeEvent(evt);
        }

        #endregion
    }
}

