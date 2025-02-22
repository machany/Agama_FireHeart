using Agama.Scripts.Enemies;
using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;
using static Agama.Scripts.Enemies.Sheep;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/Change to State")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "Change to State", message: "change to [State]", category: "Events", id: "990463180cc65f95736152e1d25b5afb")]
public partial class ChangeToState : EventChannelBase
{
    public delegate void ChangeToStateEventHandler(SheepState State);
    public event ChangeToStateEventHandler Event; 

    public void SendEventMessage(SheepState State)
    {
        Event?.Invoke(State);
    }

    public override void SendEventMessage(BlackboardVariable[] messageData)
    {
        BlackboardVariable<SheepState> StateBlackboardVariable = messageData[0] as BlackboardVariable<SheepState>;
        var State = StateBlackboardVariable != null ? StateBlackboardVariable.Value : default(SheepState);

        Event?.Invoke(State);
    }

    public override Delegate CreateEventHandler(BlackboardVariable[] vars, System.Action callback)
    {
        ChangeToStateEventHandler del = (State) =>
        {
            BlackboardVariable<SheepState> var0 = vars[0] as BlackboardVariable<SheepState>;
            if(var0 != null)
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

