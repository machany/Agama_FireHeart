using Agama.Scripts.Events;

namespace Scripts.EventChannel
{
    public enum UIType
    {
        None,
        Inventory,
        CraftingTable,
        Bonfire,
        Config
    }
    public static class UIEvents
    {
        public readonly static OpenUI OpenEvent = new OpenUI();
        public readonly static CloseUI CloseEvent = new CloseUI();
    }
    public class OpenUI : GameEvent
    {
        public UIType type;
    }
    public class CloseUI : GameEvent
    {

    }
}
