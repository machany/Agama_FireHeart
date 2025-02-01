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
            {
                if (result.gameObject.TryGetComponent(out ItemSlotUI slot))
                {
                    InventoryItem another = slot.item;
                    if (another != null)
                    {
                        if (another.data == item.data)
                            item.stackSize += another.stackSize;//maxstack 초과 시 처리 구현
                        else
                            origin.UpdateSlot(another);
                    }
                    slot.UpdateSlot(item);
                    return;
                }
            }
            origin.UpdateSlot(item);
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
