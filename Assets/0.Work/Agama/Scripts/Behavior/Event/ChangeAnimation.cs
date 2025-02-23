using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/Change Animation")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "Change Animation", message: "change animation to [Paramiter]", category: "Events", id: "d80dd681ae19bdb3f1496006be80d155")]
public partial class ChangeAnimation : EventChannelBase
{
    public delegate void ChangeAnimationEventHandler(string Paramiter);
    public event ChangeAnimationEventHandler Event; 

    public void SendEventMessage(string Paramiter)
    {
        Event?.Invoke(Paramiter);
    }

    public override void SendEventMessage(BlackboardVariable[] messageData)
    {
        BlackboardVariable<string> ParamiterBlackboardVariable = messageData[0] as BlackboardVariable<string>;
        var Paramiter = ParamiterBlackboardVariable != null ? ParamiterBlackboardVariable.Value : default(string);

        Event?.Invoke(Paramiter);
    }

    public override Delegate CreateEventHandler(BlackboardVariable[] vars, System.Action callback)
    {
        ChangeAnimationEventHandler del = (Paramiter) =>
        {
            BlackboardVariable<string> var0 = vars[0] as BlackboardVariable<string>;
            if(var0 != null)
                var0.Value = Paramiter;

            callback();
        };
        return del;
    }

    public override void RegisterListener(Delegate del)
    {
        Event += del as ChangeAnimationEventHandler;
    }

    public override void UnregisterListener(Delegate del)
    {
        Event -= del as ChangeAnimationEventHandler;
    }
}

