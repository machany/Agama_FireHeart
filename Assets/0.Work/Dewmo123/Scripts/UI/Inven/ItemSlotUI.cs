using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Scripts.UI.Inven
{
    public class ItemSlotUI : ItemUI, IDragHandler,IBeginDragHandler
    {
        [SerializeField] private GameObject _dragUI;

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (item == null) return;
            var drag = Instantiate(_dragUI, transform.root);
            var dragUI = drag.GetComponent<DragItemUI>();
            dragUI.Init(this, item);
            CleanUpSlot();
        }

        public void OnDrag(PointerEventData eventData)
        {
        }
    }
}
