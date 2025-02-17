using Agama.Scripts.Events;
using System;

namespace Scripts.EventChannel
{
    public static class PlayerUtileEvents
    {
        public readonly static RequestCook ReqCookEvent = new RequestCook();
        public readonly static RequestScoopWater ReqScoopEvent = new RequestScoopWater();
    }
    public class RequestScoopWater : GameEvent { }
    public class RequestCook : GameEvent
    {
    }
}
