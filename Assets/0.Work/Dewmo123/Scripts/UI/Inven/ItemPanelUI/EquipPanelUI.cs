using Agama.Scripts.Events;
using Scripts.EventChannel;
using Scripts.Items;
using Scripts.UI.Inven.SlotUI;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.UI.Inven.ItemPanelUI
{
    public class EquipPanelUI : PanelUI
    {
        [SerializeField] protected EventChannelSO _invenChannel;
        public Dictionary<EquipType, InventoryItem> equipments;
        protected Dictionary<EquipType, EquipSlotUI> _equipSlots;

        private void Awake()
        {
            _equipSlots = new Dictionary<EquipType, EquipSlotUI>();
            _invenChannel.AddListener<EquipData>(HandleDataRefresh);
            _slotParent.GetComponentsInChildren<EquipSlotUI>().ToList().ForEach(slot =>
            {
                _equipSlots.Add(slot.equipType, slot);
            });
        }
        private void OnDestroy()
        {
            _invenChannel.RemoveListener<EquipData>(HandleDataRefresh);
        }
        protected override void HandleDataRefresh(DataEvent evt)
        {
            var equip = evt as EquipData;
            equipments = equip.equipments;
            UpdateSlotUI();
        }

        protected override void UpdateSlotUI()
        {
            foreach (var slot in _equipSlots.Values)
            {
                slot.CleanUpSlot();
            }
            //foreach (var equipKVP in equipments)
            //Debug.Log(equipKVP.Value.data);
            foreach (var equipKVP in equipments)
            {
                _equipSlots[equipKVP.Key].UpdateSlot(equipKVP.Value);
            }
        }
    }
}
