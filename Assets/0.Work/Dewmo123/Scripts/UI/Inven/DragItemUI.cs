using Agama.Scripts.Events;
using Scripts.EventChannel;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts.UI.Inven
{
    //드래그 해서 놓았을때 그 위치의 인덱스로 교체해줘야함 원래거를
    public class DragItemUI : ItemUI, IDropHandler
    {
        public ItemUI origin;
        private RectTransform rTransform;
        [SerializeField] private EventChannelSO _invenSwapEvent;

        public void Init(ItemSlotUI origin, InventoryItem item)
        {
            UpdateSlot(item);
            rTransform = transform as RectTransform;
            transform.position = Input.mousePosition;
            this.origin = origin;
        }
        public void CheckUIUnderMouse()
        {
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerEventData, results);
            // 클릭된 오브젝트들이 results에 저장됨
            foreach (var result in results)
                if (result.gameObject.TryGetComponent(out ItemUI slot))
                {
                    if (!CheckOtherSlot<EquipSlotUI>(slot, InvokeEquipEvent, InvokeUnEquipEvent) && !CheckOtherSlot<QuickSlotUI>(slot, InvokeSetQuickEvent, InvokeUnsetQuickEvent))
                        InvokeSwapEvent(slot);
                    return;
                }
            origin.UpdateSlot(item);
        }
        private bool CheckOtherSlot<T>(ItemUI slot, Action<T> equipAction, Action<ItemUI> unEquipActoin) where T : ItemSlotUI
        {
            bool isUnEquip = origin is T;
            bool isEquip = slot is T;
            if (isUnEquip == isEquip)
                return false;

            if (isEquip)
                equipAction(slot as T);
            else if (isUnEquip)
                unEquipActoin(slot);
            return true;
        }




        #region Invoke Event
        private void InvokeSetQuickEvent(QuickSlotUI quickSlotUI)
        {
            var evt = InvenEvents.SetQuickSlotEvent;
            evt.isUnSet = false;
            evt.slotIndex = origin.slotIndex;
            evt.quickSlotIndex = quickSlotUI.slotIndex;
            if (quickSlotUI.item == null|| quickSlotUI.item.data == null)
                evt.isSame = false;
            else
                evt.isSame = quickSlotUI.item.data == item.data;
            _invenSwapEvent.InvokeEvent(evt);
        }
        private void InvokeUnsetQuickEvent(ItemUI slot)
        {

            var evt = InvenEvents.SetQuickSlotEvent;
            var quickSlotUI = origin as QuickSlotUI;
            evt.isUnSet = true;
            evt.slotIndex = slot.slotIndex;
            evt.quickSlotIndex = quickSlotUI.slotIndex;
            if (slot.item == null)
                evt.isSame = false;
            else
                evt.isSame = item.data == slot.item.data;
            _invenSwapEvent.InvokeEvent(evt);
            
        }
        private void InvokeEquipEvent(EquipSlotUI slot)
        {
            var equipEvent = InvenEvents.EquipEvent;
            equipEvent.isUnEquip = false;
            equipEvent.type = slot.equipType;
            equipEvent.index1 = origin.slotIndex;
            _invenSwapEvent.InvokeEvent(equipEvent);
        }
        private void InvokeUnEquipEvent(ItemUI slot)
        {
            var equipEvent = InvenEvents.EquipEvent;
            var originSlot = origin as EquipSlotUI;
            equipEvent.isUnEquip = true;
            equipEvent.type = originSlot.equipType;
            equipEvent.index1 = slot.slotIndex;
            _invenSwapEvent.InvokeEvent(equipEvent);
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
            _invenSwapEvent.InvokeEvent(swapEvent);
        }
        #endregion
        public void OnDrop(PointerEventData eventData)
        {
            CheckUIUnderMouse();
            Destroy(gameObject);
        }


        private void Update()
        {
            transform.position = Input.mousePosition;
        }

    }
}
