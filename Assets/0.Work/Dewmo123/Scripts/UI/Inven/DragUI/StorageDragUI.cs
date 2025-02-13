using Scripts.EventChannel;
using Scripts.UI.Inven.SlotUI;

namespace Scripts.UI.Inven.DragUI
{
    public class StorageDragUI : DragItemUI
    {
        public override bool CheckOtherSlot(ItemUI slot)
        {
            if(slot is StorageSlotUI)
            {
                InvokeChangeStorageEvent(slot as StorageSlotUI);
                return true;
            }
            else if(slot is InvenSlotUI)
            {
                InvokeUnsetQuickEvent(slot as InvenSlotUI);
                return true;
            }
            return false;
        }
        private void InvokeChangeStorageEvent(StorageSlotUI storage)
        {
            var evt = InvenEvents.SetStorageSlotEvent;
            var origin = this.origin as StorageSlotUI;
            evt.isSwap = true;
            evt.storageSlotIndex = origin.slotIndex;
            evt.storageSlotIndex2 = storage.slotIndex;
            _invenEvent.InvokeEvent(evt);
        }
        private void InvokeUnsetQuickEvent(InvenSlotUI slot)
        {
            var evt = InvenEvents.SetStorageSlotEvent;
            var storageUI = origin as StorageSlotUI;
            evt.isSwap = false;
            evt.isUnSet = true;
            evt.slotIndex = slot.slotIndex;
            evt.storageSlotIndex = storageUI.slotIndex;
            if (slot.item == null)
                evt.isSame = false;
            else
                evt.isSame = item.data == slot.item.data;
            _invenEvent.InvokeEvent(evt);

        }
    }
}
