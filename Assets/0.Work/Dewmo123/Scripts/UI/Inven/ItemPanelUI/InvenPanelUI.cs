using Agama.Scripts.Events;
using Scripts.Core;
using Scripts.EventChannel;
using Scripts.Items;
using Scripts.UI.Inven.SlotUI;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.UI.Inven.ItemPanelUI
{
    //아마 오픈, 클로즈도 만들어야할
    public class InvenPanelUI : PanelUI
    {
        [SerializeField] protected EventChannelSO _invenChannel;
        public List<InventoryItem> inventory;

        protected ItemSlotUI[] _itemSlots;
        public void Awake()
        {
            _invenChannel.AddListener<InvenData>(HandleDataRefresh);

            _itemSlots = _slotParent.GetComponentsInChildren<ItemSlotUI>();


            for (int i = 0; i < _itemSlots.Length; i++)
                _itemSlots[i].slotIndex = i;
        }
        private void OnDestroy()
        {
            _invenChannel.RemoveListener<InvenData>(HandleDataRefresh);
        }

        protected override void HandleDataRefresh(DataEvent evt)
        {
            var inven = evt as InvenData;
            inventory = inven.items;
            UpdateSlotUI();                                                                     
        }

        /// <summary>
        /// Inventory is reflected in UI
        /// </summary>
        protected override void UpdateSlotUI()
        {
            for (int i = 0; i < _itemSlots.Length; i++)
            {
                _itemSlots[i].CleanUpSlot();
            }
            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].data != null)
                    _itemSlots[i].UpdateSlot(inventory[i]);
            }
        }
    }
}
