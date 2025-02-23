using Agama.Scripts.Entities;
using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "wait dead event", story: "[Entity] wait dead event", category: "Action", id: "337c87ff41090f0fd5c312a649253ff3")]
public partial class WaitDeadEventAction : Action
{
    [SerializeReference] public BlackboardVariable<Entity> Entity;

    private bool _triggerCall;

    protected override Status OnStart()
    {
        Entity.Value.OnHitEvent.AddListener(HandleAnimationEndEvent);
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (_triggerCall)
            return Status.Success;
        return Status.Running;
    }

    private void HandleAnimationEndEvent()
        => _triggerCall = true;

    protected override void OnEnd()
    {
        Entity.Value.OnHitEvent.RemoveListener(HandleAnimationEndEvent);
    }
}

