using Agama.Scripts.Entities;
using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace Agama.Scripts.Behavior.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Wait Animation End", story: "wait for [AnimatorTrigger] end event", category: "Action", id: "6016faadd3b0f2851f9e0912fcf29956")]
    public partial class WaitAnimationEndAction : Action
    {
        [SerializeReference] public BlackboardVariable<EntityAnimatorTrigger> AnimatorTrigger;

        private bool _triggerCall;

        protected override Status OnStart()
        {
            _triggerCall = false;
            AnimatorTrigger.Value.OnAnimationEndEvent += HandleAnimationEndEvent;
            return Status.Running;
        }

        private void HandleAnimationEndEvent()
            => _triggerCall = true;

        protected override Status OnUpdate()
        {
            if (_triggerCall)
                return Status.Success;
            return Status.Running;
        }

        protected override void OnEnd()
        {
            AnimatorTrigger.Value.OnAnimationEndEvent -= HandleAnimationEndEvent;
        }
    }
}

