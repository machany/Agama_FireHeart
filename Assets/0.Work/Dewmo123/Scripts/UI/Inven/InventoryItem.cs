﻿using Scripts.Items;
using System;

namespace Scripts.UI.Inven
{
    [Serializable]
    public class InventoryItem
    {
        public ItemDataSO data;
        public int stackSize;

        public bool isFullStack => stackSize >= data.maxStack;

        public InventoryItem(ItemDataSO newItemData, int count = 1)
        {
            data = newItemData;
            stackSize = count;
        }
        public int AddStack(int count)
        {
            int remainCount = 0;
            stackSize += count;
            if (stackSize > data.maxStack)
            {
                remainCount = stackSize - data.maxStack;
                stackSize = data.maxStack;
            }
            return remainCount;
        }
        public void RemoveStack(int count = 1)
        {
            stackSize -= count;
        }

    }
}
