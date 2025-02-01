using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts.UI.Inven
{
    //드래그 해서 놓았을때 그 위치의 인덱스로 교체해줘야함 원래거를
    public class DragItemUI : ItemUI, IDropHandler
    {
        public ItemSlotUI origin;

        public void Init(ItemSlotUI origin, InventoryItem item)
        {
            UpdateSlot(item);
            transform.position = Input.mousePosition;
            this.origin = origin;
        }


        public void OnDrop(PointerEventData eventData)
        {
            var item = eventData.pointerDrag.GetComponent<ItemUI>();
            item.UpdateSlot(this.item);
            Destroy(gameObject);
        }


        private void Update()
        {
            transform.position = Input.mousePosition;
        }

    }
}
