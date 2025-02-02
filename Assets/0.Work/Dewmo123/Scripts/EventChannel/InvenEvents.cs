using Agama.Scripts.Events;

namespace Scripts.EventChannel
{
    public static class InvenEvents
    {
        public static readonly InvenSwapEvent SwapEvent = new InvenSwapEvent();
    }
    public class InvenSwapEvent : GameEvent
    {
        public bool isSame;
        public int index1, index2;
    }
}
