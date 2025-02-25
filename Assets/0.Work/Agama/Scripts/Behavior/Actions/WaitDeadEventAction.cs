using Agama.Scripts.Entities;
using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace Agama.Scripts.Behavior.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "wait dead event", story: "[Entity] wait dead event", category: "Action", id: "337c87ff41090f0fd5c312a649253ff3")]
    public partial class WaitDeadEventAction : Action
    {
        [SerializeReference] public BlackboardVariable<Entity> Entity;

        private bool _triggerCall;

        protected override Status OnStart()
        {
            Entity.Value.OnDeadEvent.AddListener(HandleDeadEvent);
            _triggerCall = false;
            return Status.Running;
        }

        protected override Status OnUpdate()
        {
            if (_triggerCall)
                return Status.Success;
            return Status.Running;
        }

        private void HandleDeadEvent()
            => _triggerCall = true;

        protected override void OnEnd()
        {
            Entity.Value.OnDeadEvent.RemoveListener(HandleDeadEvent);
        }
    }
}
