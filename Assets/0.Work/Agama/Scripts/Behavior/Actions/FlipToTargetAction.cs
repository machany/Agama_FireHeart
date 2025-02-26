using Agama.Scripts.Entities;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

namespace Agama.Scripts.Behavior.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Flip to Target", story: "[Transform] flip [Renderer] to [Target]", category: "Action", id: "862175652b1ed3e7538cc5b4fd4f1c67")]
    public partial class FlipToTargetAction : Action
    {
        [SerializeReference] public BlackboardVariable<Transform> Transform;
        [SerializeReference] public BlackboardVariable<EntityRenderer> Renderer;
        [SerializeReference] public BlackboardVariable<Transform> Target;

        protected override Status OnStart()
        {
            Renderer.Value.Flip((Target.Value.position - Transform.Value.position).x);
            return Status.Success;
        }
    }
}
