using Scripts.EventChannel;
using Scripts.UI.Inven.SlotUI;

namespace Scripts.UI.Inven.DragUI
{
    public class InvenDragUI : DragItemUI
    {
        public override bool CheckOtherSlot(ItemUI slot)
        {
            if (slot is QuickSlotUI)
            {
                InvokeSetQuickEvent(slot as QuickSlotUI);
                return true;
            }
            else if (slot is EquipSlotUI)
            {
                InvokeEquipEvent(slot as EquipSlotUI);
                return true;
            }
            else if(slot is InvenSlotUI)
            {
                InvokeSwapEvent(slot);
                return true;
            }

            return false;
        }
        private void InvokeSetQuickEvent(QuickSlotUI quickSlotUI)
        {
            var evt = InvenEvents.SetQuickSlotEvent;
            evt.isSwap = false;
            evt.isUnSet = false;
            evt.slotIndex = origin.slotIndex;
            evt.quickSlotIndex = quickSlotUI.slotIndex;
            if (quickSlotUI.item == null || quickSlotUI.item.data == null)
                evt.isSame = false;
            else
                evt.isSame = quickSlotUI.item.data == item.data;
            _invenEvent.InvokeEvent(evt);
        }
        private void InvokeEquipEvent(EquipSlotUI slot)
        {
            var equipEvent = InvenEvents.EquipEvent;
            equipEvent.isUnEquip = false;
            equipEvent.type = slot.equipType;
            equipEvent.index1 = origin.slotIndex;
            _invenEvent.InvokeEvent(equipEvent);
        }
        private void InvokeSwapEvent(ItemUI slot)
        {
            InventoryItem another = slot.item;
            var swapEvent = InvenEvents.SwapEvent;
            if (another == null || another.data != item.data)
                swapEvent.isSame = false;
            else
                swapEvent.isSame = true;
            swapEvent.index1 = origin.slotIndex;
            swapEvent.index2 = slot.slotIndex;
            _invenEvent.InvokeEvent(swapEvent);
        }
    }
}
