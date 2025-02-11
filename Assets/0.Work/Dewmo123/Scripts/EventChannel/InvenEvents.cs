using Agama.Scripts.Events;
using Scripts.Items;
using Scripts.UI.Inven;
using System.Collections.Generic;

namespace Scripts.EventChannel
{
    public static class InvenEvents
    {
        public static readonly InvenSwap SwapEvent = new InvenSwap();
        public static readonly InvenEquip EquipEvent = new InvenEquip();

        public static readonly InvenData DataEvent = new InvenData();

        public static readonly RequestInvenData RequestDataEvent = new RequestInvenData();

        public static readonly SetQuickSlot SetQuickSlotEvent = new SetQuickSlot();
        public static readonly SetStorageSlot SetStorageSlotEvent = new SetStorageSlot();

        public static readonly QuickSlotData QuickSlotDataEvent = new QuickSlotData();
        public static readonly StorageData StorageDataEvent = new StorageData();

        public static readonly CraftItem CraftItemEvent = new CraftItem();
    }
    public class InvenData : GameEvent
    {
        public int slotCount;
        public List<InventoryItem> items;
        public Dictionary<EquipType, InventoryItem> equipments;
    }
    public class QuickSlotData : GameEvent
    {
        public List<InventoryItem> quickSlotItems;
    }
    public class StorageData : GameEvent
    {
        public List<InventoryItem> storage;
    }
    public class RequestInvenData : GameEvent
    {
    }
    public class SetQuickSlot : GameEvent
    {
        public bool isSame, isUnSet,isSwap = false;
        public int slotIndex, quickSlotIndex,quickSlotIndex2;
    }
    public class SetStorageSlot : GameEvent
    {
        public bool isSame, isUnSet, isSwap = false;
        public int slotIndex, storageSlotIndex, storageSlotIndex2;
    }
    public class InvenSwap : GameEvent
    {
        public bool isSame;
        public int index1, index2;
    }
    public class InvenEquip : GameEvent
    {
        public bool isUnEquip;
        public int index1;
        public EquipType type;
    }
    public class CraftItem : GameEvent
    {
        public CraftingRecipeSO recipe;
    }
}
