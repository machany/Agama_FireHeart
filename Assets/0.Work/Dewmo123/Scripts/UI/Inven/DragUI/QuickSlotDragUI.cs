using Scripts.EventChannel;
using Scripts.UI.Inven.SlotUI;

namespace Scripts.UI.Inven.DragUI
{
    public class QuickSlotDragUI : DragItemUI
    {
        public override bool CheckOtherSlot(ItemUI slot)
        {
            if (slot is QuickSlotUI)
            {
                InvokeChangeQuickEvent(slot as QuickSlotUI);
                return true;
            }
            else if (slot is InvenSlotUI)
            {
                InvokeUnsetQuickEvent(slot);
                return true;
            }
            return false;
        }
        private void InvokeChangeQuickEvent(QuickSlotUI quickSlot)
        {
            var evt = InvenEvents.SetQuickSlotEvent;
            var origin = this.origin as QuickSlotUI;
            evt.isSwap = true;
            evt.quickSlotIndex = origin.slotIndex;
            evt.quickSlotIndex2 = quickSlot.slotIndex;
            _invenEvent.InvokeEvent(evt);
        }

        private void InvokeUnsetQuickEvent(ItemUI slot)
        {
            var evt = InvenEvents.SetQuickSlotEvent;
            var quickSlotUI = origin as QuickSlotUI;
            evt.isSwap = false;
            evt.isUnSet = true;
            evt.slotIndex = slot.slotIndex;
            evt.quickSlotIndex = quickSlotUI.slotIndex;
            if (slot.item == null)
                evt.isSame = false;
            else
                evt.isSame = item.data == slot.item.data;
            _invenEvent.InvokeEvent(evt);

        }
    }
}
