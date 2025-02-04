using Scripts.Items;
using Scripts.UI.Inven;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.InvenSystem
{
    public abstract class InvenData : MonoBehaviour
    {
        public List<InventoryItem> inventory;

        public virtual InventoryItem GetItem(ItemDataSO itemData) => inventory.FirstOrDefault(inventoryItem => inventoryItem.data == itemData);
        public virtual IEnumerable<InventoryItem> GetItems(ItemDataSO itemData) => inventory.Where(inventoryItem => inventoryItem.data == itemData);

        public virtual int GetAllItemStack(ItemDataSO itemData)
        {
            var items = GetItems(itemData);
            int sum = 0;
            foreach (var item in items)
                sum += item.stackSize;
            return sum;
        }

        public abstract void AddItem(ItemDataSO itemData, int count = 1);
        public abstract void RemoveItem(ItemDataSO itemData, int count);
        public abstract bool CanAddItem(ItemDataSO itemData);

    }
}
