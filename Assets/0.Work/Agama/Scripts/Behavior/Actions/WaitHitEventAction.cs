using Agama.Scripts.Entities;
using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace Agama.Scripts.Behavior.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Wait Hit Event", story: "[Entity] wait hit event", category: "Action", id: "b53ee5a73ba6dd42efb95bfe2bb11f0f")]
    public partial class WaitHitEventAction : Action
    {
        [SerializeReference] public BlackboardVariable<Entity> Entity;

        private bool _triggerCall;

        protected override Status OnStart()
        {
            Entity.Value.OnHitEvent.AddListener(HandleHitEvent);
            _triggerCall = false;
            return Status.Running;
        }

        protected override Status OnUpdate()
        {
            if (_triggerCall)
                return Status.Success;
            return Status.Running;
        }

        private void HandleHitEvent()
            => _triggerCall = true;

        protected override void OnEnd()
        {
            Entity.Value.OnHitEvent.RemoveListener(HandleHitEvent);
        }
    }
}

