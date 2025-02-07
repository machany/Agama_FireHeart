using Agama.Scripts.Events;

namespace Scripts.EventChannel
{
    public static class PlayerUtileEvents
    {
        public readonly static RequestCook ReqCookEvent = new RequestCook();
    }
    public class RequestCook : GameEvent
    {

    }
}
