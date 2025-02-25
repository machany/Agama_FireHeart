using Agama.Scripts.Entities;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

namespace Agama.Scripts.Behavior.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Flip as MoveDirection", story: "flip [Renderer] as [MoveDirection]", category: "Action", id: "f56ee22e00a40e88764e93d99606ab8c")]
    public partial class FlipAsMoveDirectionAction : Action
    {
        [SerializeReference] public BlackboardVariable<EntityRenderer> Renderer;
        [SerializeReference] public BlackboardVariable<Vector2> MoveDirection;

        protected override Status OnStart()
        {
            Renderer.Value.Flip(MoveDirection.Value.x);
            return Status.Success;
        }
    }
}
