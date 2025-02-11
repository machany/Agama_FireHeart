using Agama.Scripts.Events;
using Scripts.EventChannel;
using Scripts.UI.Inven.SlotUI;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.UI.Inven.ItemPanelUI
{
    public class QuickSlotPanelUI : PanelUI
    {
        [SerializeField] protected EventChannelSO _invenChannel;
        public List<InventoryItem> quickSlotItems;
        protected QuickSlotUI[] _quickSlots;

        private void Awake()
        {
            _quickSlots = _slotParent.GetComponentsInChildren<QuickSlotUI>();
            _invenChannel.AddListener<QuickSlotData>(HandleDataRefresh);

            for (int i = 0; i < _quickSlots.Length; i++)
                _quickSlots[i].slotIndex = i;
            
            UpdateSlotUI();
        }
        private void OnDestroy()
        {
            _invenChannel.RemoveListener<QuickSlotData>(HandleDataRefresh);
        }
        protected override void HandleDataRefresh(DataEvent evt)
        {
            var quick = evt as QuickSlotData;
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
