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
    }
    public class InvenData : GameEvent
    {
        public int slotCount;
        public List<InventoryItem> items;
        public Dictionary<EquipType, InventoryItem> equipments;
    }
    public class RequestInvenData : GameEvent
    {
    }
    public class SetQuickSlot : GameEvent
    {
        public bool isSame, isUnSet;
        public int slotIndex, quickSlotIndex;
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
}
