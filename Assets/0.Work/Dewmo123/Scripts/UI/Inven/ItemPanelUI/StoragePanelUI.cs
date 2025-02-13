using Agama.Scripts.Events;
using Scripts.EventChannel;
using Scripts.UI.Inven.SlotUI;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.UI.Inven.ItemPanelUI
{
    public class StoragePanelUI : PanelUI
    {
        [SerializeField] protected EventChannelSO _invenChannel;
        public List<InventoryItem> storageItems;
        private StorageSlotUI[] _storage;
        private void Awake()
        {
            _storage = _slotParent.GetComponentsInChildren<StorageSlotUI>();
            _invenChannel.AddListener<StorageData>(HandleDataRefresh);
            for (int i = 0; i < _storage.Length; i++)
                _storage[i].slotIndex = i;
        }

        private void OnDestroy()
        {
            _invenChannel.RemoveListener<StorageData>(HandleDataRefresh);
        }
        protected override void HandleDataRefresh(DataEvent evt)
        {
            var storage = evt as StorageData;
            storageItems = storage.storage;
            UpdateSlotUI();
        }

        protected override void UpdateSlotUI()
        {
            for (int i = 0; i < _storage.Length; i++)
            {
                _storage[i].CleanUpSlot();
            }
            for (int i = 0; i < storageItems.Count; i++)
            {
                if (storageItems[i].data != null)
                    _storage[i].UpdateSlot(storageItems[i]);
            }
        }
    }
}
