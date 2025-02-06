using Agama.Scripts.Events;
using Scripts.Core;
using Scripts.EventChannel;
using Scripts.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.UI.Inven
{
    //아마 오픈, 클로즈도 만들어야할
    public class InvenPanelUI : MonoBehaviour
    {
        [SerializeField] protected EventChannelSO _invenChannel;
        public List<InventoryItem> inventory;

        [SerializeField] protected Transform _slotParent;
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

        private void HandleDataRefresh(InvenData evt)//얘한테 줄때 현재 슬롯카운트 크기의 리스트로 줌
        {
            inventory = evt.items;
            UpdateSlotUI();
        }
        /// <summary>
        /// Inventory is reflected in UI
        /// </summary>
        public virtual void UpdateSlotUI()
        {
            for (int i = 0; i < _itemSlots.Length; i++)
            {
                _itemSlots[i].CleanUpSlot();
            }
            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i] == null) continue;
                if (inventory[i].data != null)
                    _itemSlots[i].UpdateSlot(inventory[i]);
            }
        }
    }
}
