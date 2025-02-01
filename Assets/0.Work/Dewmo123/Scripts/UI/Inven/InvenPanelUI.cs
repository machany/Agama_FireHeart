using Scripts.Core;
using Scripts.InvenSystem;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.UI.Inven
{
    public class InvenPanelUI : MonoBehaviour
    {
        //[SerializeField] protected GameEventChannelSO _inventoryChannel;

        public List<InventoryItem> inventory;

        [SerializeField] protected Transform _slotParent;
        protected ItemSlotUI[] _itemSlots;

        protected virtual void Awake()
        {
            _itemSlots = _slotParent.GetComponentsInChildren<ItemSlotUI>();
            for(int i = 0; i < _itemSlots.Length; i++)
            {
                _itemSlots[i].slotIndex = i;
            }
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
                HandleDataRefresh(inventory);
        }
        //public override void Open()
        //{
        //    base.Open();
        //    _inventoryChannel.AddListener<InventoryData>(HandleDataRefresh);
        //    _inventoryChannel.RaiseEvent(InventoryEvents.RequestInventoryData);
        //}

        //public override void Close()
        //{
        //    base.Close();
        //    _inventoryChannel.RemoveListener<InventoryData>(HandleDataRefresh);
        //}

        private void HandleDataRefresh(List<InventoryItem> evt)//얘한테 줄때 현재 슬롯카운트 크기의 리스트로 줌
        {
            inventory = evt;
            UpdateSlotUI();
        }

        public virtual void UpdateSlotUI()
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
