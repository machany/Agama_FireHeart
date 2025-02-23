using Agama.Scripts.Entities;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

namespace Agama.Scripts.Behavior.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Move Mover", story: "[Mover] move [Diraction]", category: "Action", id: "8b08dec00a16d9e5e6314a11db6630aa")]
    public partial class MoveMoverAction : Action
    {
        [SerializeReference] public BlackboardVariable<EntityMover> Mover;
        [SerializeReference] public BlackboardVariable<Vector2> Diraction;

        protected override Status OnStart()
        {
            Mover.Value.SetMovement(Diraction.Value);
            return Status.Success;
        }
    }
}

