using Agama.Scripts.Enemies;
using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;
using static Agama.Scripts.Enemies.Animal;
using static Agama.Scripts.Enemies.BehaviorEnemy;

namespace Agama.Scripts.Behavior.Events
{
#if UNITY_EDITOR
    [CreateAssetMenu(menuName = "Behavior/Event Channels/Change to State")]
#endif
    [Serializable, GeneratePropertyBag]
    [EventChannelDescription(name: "Change Behavior Enemy State Event", message: "change to [State]", category: "Events", id: "990463180cc65f95736152e1d25b5afb")]
    public partial class ChangeBehaviorEnemyStateState : EventChannelBase
    {
        public delegate void ChangeToStateEventHandler(BehaviorEnemyState State);
        public event ChangeToStateEventHandler Event;

        public void SendEventMessage(BehaviorEnemyState State)
        {
            Event?.Invoke(State);
        }

        public override void SendEventMessage(BlackboardVariable[] messageData)
        {
            BlackboardVariable<BehaviorEnemyState> StateBlackboardVariable = messageData[0] as BlackboardVariable<BehaviorEnemyState>;
            var State = StateBlackboardVariable != null ? StateBlackboardVariable.Value : default(BehaviorEnemyState);

            Event?.Invoke(State);
        }

        public override Delegate CreateEventHandler(BlackboardVariable[] vars, System.Action callback)
        {
            ChangeToStateEventHandler del = (State) =>
            {
                BlackboardVariable<BehaviorEnemyState> var0 = vars[0] as BlackboardVariable<BehaviorEnemyState>;
                if (var0 != null)
                    var0.Value = State;

                callback();
            };
            return del;
        }

        public override void RegisterListener(Delegate del)
        {
            Event += del as ChangeToStateEventHandler;
        }

        public override void UnregisterListener(Delegate del)
        {
            Event -= del as ChangeToStateEventHandler;
        }
    }
}

