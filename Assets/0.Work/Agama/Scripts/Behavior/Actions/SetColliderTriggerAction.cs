using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

namespace Agama.Scripts.Behavior.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Set Collider Trigger", story: "set [Collider] trigger [Value]", category: "Action", id: "24b0c4659c2a7b165b846fcaa25547cf")]
    public partial class SetColliderTriggerAction : Action
    {
        [SerializeReference] public BlackboardVariable<Collider2D> Collider;
        [SerializeReference] public BlackboardVariable<bool> Value;

        protected override Status OnStart()
        {
            if (Collider.Value == null)
                return Status.Failure;

            Collider.Value.isTrigger = Value.Value;
            return Status.Success;
        }
    }
}
