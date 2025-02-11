using Scripts.EventChannel;
using Scripts.UI.Inven.SlotUI;

namespace Scripts.UI.Inven.DragUI
{
    public class EquipDragUI : DragItemUI
    {
        public override bool CheckOtherSlot(ItemUI slot)
        {
            if(slot is InvenSlotUI)
            {
                InvokeUnEquipEvent(slot);
                return true;
            }
            return false;
        }

        private void InvokeUnEquipEvent(ItemUI slot)
        {
            var equipEvent = InvenEvents.EquipEvent;
            var originSlot = origin as EquipSlotUI;
            equipEvent.isUnEquip = true;
            equipEvent.type = originSlot.equipType;
            equipEvent.index1 = slot.slotIndex;
            _invenEvent.InvokeEvent(equipEvent);
        }
    }
}
