using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Inven
{
    public class ItemUI : MonoBehaviour
    {
        [SerializeField] protected Image _itemImage;
        [SerializeField] protected TextMeshProUGUI _itemText;
        public int slotIndex;
        public InventoryItem item;
        public virtual void Init(int index)
        {
            slotIndex = index;
        }
        public virtual void UpdateSlot(InventoryItem newItem)
        {
            item = newItem;
            _itemImage.color = Color.white;
            if (item != null)
            {
                _itemImage.sprite = item.data.icon;
                if (item.stackSize > 1)
                    _itemText.text = item.stackSize.ToString();
                else
                    _itemText.text = string.Empty;
            }
        }
        public void CleanUpSlot()
        {
            item = null;
            _itemImage.sprite = null;
            _itemImage.color = Color.clear;

            _itemText.text = string.Empty;
        }
    }
}
