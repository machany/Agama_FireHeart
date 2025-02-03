using Agama.Scripts.Events;

namespace Scripts.EventChannel
{
    public static class CraftEvents
    {
        public readonly static  CraftInfo CraftInfoEvent = new CraftInfo(); 
    }
    public class CraftInfo : GameEvent
    {
        public string title, description;
    }
}
