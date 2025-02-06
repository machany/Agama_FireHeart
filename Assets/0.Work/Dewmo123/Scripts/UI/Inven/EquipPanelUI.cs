using Agama.Scripts.Events;
using Scripts.EventChannel;
using Scripts.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.UI.Inven
{
    public class EquipPanelUI : MonoBehaviour
    {
        [SerializeField] protected EventChannelSO _invenChannel;
        public Dictionary<EquipType, InventoryItem> equipments;
        protected Dictionary<EquipType, EquipSlotUI> _equipSlots;

        [SerializeField] protected Transform  _equipSlotParent;
        private void Awake()
        {
            _equipSlots = new Dictionary<EquipType, EquipSlotUI>();
            _invenChannel.AddListener<InvenData>(HandleDataRefresh);
            _equipSlotParent.GetComponentsInChildren<EquipSlotUI>().ToList().ForEach(slot =>
            {
                _equipSlots.Add(slot.equipType, slot);
            });
        }
        private void OnDestroy()
        {
            _invenChannel.RemoveListener<InvenData>(HandleDataRefresh);
        }
        private void HandleDataRefresh(InvenData evt)//얘한테 줄때 현재 슬롯카운트 크기의 리스트로 줌
        {
            equipments = evt.equipments;
            UpdateSlotUI();
        }

        private void UpdateSlotUI()
        {
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
