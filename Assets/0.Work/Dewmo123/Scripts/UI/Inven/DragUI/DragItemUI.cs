using Agama.Scripts.Events;
using Scripts.EventChannel;
using Scripts.UI.Inven.SlotUI;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts.UI.Inven.DragUI
{
    //드래그 해서 놓았을때 그 위치의 인덱스로 교체해줘야함 원래거를
    public abstract class DragItemUI : ItemUI, IDropHandler
    {
        public ItemUI origin;
        private RectTransform rTransform;
        [SerializeField] protected EventChannelSO _invenEvent;

        public virtual void Init(ItemSlotUI origin, InventoryItem item)
        {
            UpdateSlot(item);
            rTransform = transform as RectTransform;
            transform.position = Input.mousePosition;
            this.origin = origin;
        }
        public virtual void CheckUIUnderMouse()
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
                    if (CheckOtherSlot(slot))
                        return;
                }
            origin.UpdateSlot(item);
        }
        public abstract bool CheckOtherSlot(ItemUI slot);


        public virtual void OnDrop(PointerEventData eventData)
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
