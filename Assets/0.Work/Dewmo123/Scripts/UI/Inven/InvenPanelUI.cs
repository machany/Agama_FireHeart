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
    //여기서 인벤 관리중 플레이어에서 관리할거면 이벤트채널 받아서 리스트 수정한 후 여기에 보내주면 됨
    public class InvenPanelUI : MonoBehaviour
    {
        [SerializeField] protected EventChannelSO _invenChannel;
        public List<InventoryItem> inventory;
        public Dictionary<EquipType, InventoryItem> equipments;

        [SerializeField] protected Transform _slotParent, _equipSlotParent;
        protected Dictionary<EquipType, EquipSlotUI> _equipSlots;
        protected ItemSlotUI[] _itemSlots;

        protected virtual void Awake()
        {
            _equipSlots = new Dictionary<EquipType, EquipSlotUI>();
            _invenChannel.AddListener<InvenData>(HandleDataRefresh);
            _equipSlotParent.GetComponentsInChildren<EquipSlotUI>().ToList().ForEach(slot =>
            {
                _equipSlots.Add(slot.equipType, slot);
            });
            _itemSlots = _slotParent.GetComponentsInChildren<ItemSlotUI>();



            for(int i = 0; i < _itemSlots.Length; i++)
                _itemSlots[i].slotIndex = i;
        }
        private void OnDestroy()
        {
            _invenChannel.RemoveListener<InvenData>(HandleDataRefresh);
        }

        private void HandleDataRefresh(InvenData evt)//얘한테 줄때 현재 슬롯카운트 크기의 리스트로 줌
        {
            inventory = evt.items;
            equipments = evt.equipments;
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
            foreach (var slot in _equipSlots.Values)
            {
                slot.CleanUpSlot();
            }

            foreach (var equipKVP in equipments)
            {
                _equipSlots[equipKVP.Key].UpdateSlot(equipKVP.Value);
            }

        }
    }
}
