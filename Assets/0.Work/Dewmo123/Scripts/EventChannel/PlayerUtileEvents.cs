using Agama.Scripts.Events;
using Assets._0.Work.Dewmo123.Scripts.Structures;
using System;

namespace Scripts.EventChannel
{
    public static class PlayerUtileEvents
    {
        public readonly static RequestCook ReqCookEvent = new RequestCook();
        public readonly static RequestScoopWater ReqScoopEvent = new RequestScoopWater();
        public readonly static SelectTent SelectTentEvent = new SelectTent();
    }
    public class RequestScoopWater : GameEvent { }
    public class RequestCook : GameEvent { }
    public class SelectTent : GameEvent 
    {
        public Tent tent;
    }
}
