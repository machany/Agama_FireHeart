using Agama.Scripts.Events;
using Scripts.EventChannel;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts.UI.Inven
{
    //드래그 해서 놓았을때 그 위치의 인덱스로 교체해줘야함 원래거를
    public class DragItemUI : ItemUI, IDropHandler
    {
        public ItemSlotUI origin;
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
                if (result.gameObject.TryGetComponent(out ItemSlotUI slot))
                {
                    InvokeSwapEvent(slot);
                    return;
                }
            origin.UpdateSlot(item);
        }

        private void InvokeSwapEvent(ItemSlotUI slot)
        {
            InventoryItem another = slot.item;
            var swapEvent = InvenEvents.SwapEvent;
            if (another == null)
                swapEvent.isSame = false;
            else
                swapEvent.isSame = another.data == item.data;
            swapEvent.index1 = origin.slotIndex;
            swapEvent.index2 = slot.slotIndex;
            _invenSwapEvent.InvokeEvent(swapEvent);
        }

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
