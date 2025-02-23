using Agama.Scripts.Core;
using Agama.Scripts.Events;
using Scripts.EventChannel;
using Scripts.UI.Inven.SlotUI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.UI.Inven.ItemPanelUI
{
    public class QuickSlotPanelUI : PanelUI
    {
        [SerializeField] protected EventChannelSO _invenChannel;
        public List<InventoryItem> quickSlotItems;
        protected QuickSlotUI[] _quickSlots;
        [SerializeField] private Transform _selectUI;
        [SerializeField] private PlayerInputSO _input;

        private void Awake()
        {
            _quickSlots = _slotParent.GetComponentsInChildren<QuickSlotUI>();
            _invenChannel.AddListener<QuickSlotData>(HandleDataRefresh);
            _input.OnQuickSlotChangedEvent += HandleQuickSlotChanged;
            for (int i = 0; i < _quickSlots.Length; i++)
                _quickSlots[i].slotIndex = i;
            
            UpdateSlotUI();
        }

        private void HandleQuickSlotChanged(sbyte obj)
        {
            _selectUI.position = _quickSlots[obj].transform.position;
        }

        private void OnDestroy()
        {
            _invenChannel.RemoveListener<QuickSlotData>(HandleDataRefresh);
        }
        protected override void HandleDataRefresh(DataEvent evt)
        {
            var quick = evt as QuickSlotData;
            quickSlotItems = quick.quickSlotItems;
            UpdateSlotUI();
        }

        protected override void UpdateSlotUI()
        {
            for (int i = 0; i < quickSlotItems.Count; i++)
            {
                _quickSlots[i].CleanUpSlot();
            }
            for (int i = 0; i < quickSlotItems.Count; i++)
            {
                if (quickSlotItems[i].data != null)
                    _quickSlots[i].UpdateSlot(quickSlotItems[i]);
            }
        }
    }
}
