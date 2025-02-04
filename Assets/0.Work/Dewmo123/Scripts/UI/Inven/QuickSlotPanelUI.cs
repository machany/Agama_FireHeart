using Agama.Scripts.Events;
using Scripts.EventChannel;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.UI.Inven
{
    public class QuickSlotPanelUI : MonoBehaviour
    {
        [SerializeField] protected EventChannelSO _invenChannel;
        public List<InventoryItem> quickSlotItems;
        protected QuickSlotUI[] _quickSlots;
        [SerializeField] private Transform _quickSlotParent;
        private void Awake()
        {
            _quickSlots = _quickSlotParent.GetComponentsInChildren<QuickSlotUI>();
            _invenChannel.AddListener<QuickSlotData>(HandleDataRefresh);
            for (int i = 0; i < _quickSlots.Length; i++)
                _quickSlots[i].slotIndex = i;
            UpdateQuickSlot();
        }
        private void OnDestroy()
        {
            _invenChannel.RemoveListener<QuickSlotData>(HandleDataRefresh);
        }
        private void HandleDataRefresh(QuickSlotData evt)
        {
            quickSlotItems = evt.quickSlotItems;
            UpdateQuickSlot();
        }

        private void UpdateQuickSlot()
        {
            for (int i = 0; i < quickSlotItems.Count; i++)
            {
                _quickSlots[i].CleanUpSlot();
            }
            for (int i = 0; i < quickSlotItems.Count; i++)
            {
                if (quickSlotItems[i] == null) continue;
                if (quickSlotItems[i].data != null)
                    _quickSlots[i].UpdateSlot(quickSlotItems[i]);
            }
        }
    }
}
