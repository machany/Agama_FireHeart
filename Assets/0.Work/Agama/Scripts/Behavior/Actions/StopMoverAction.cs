using Agama.Scripts.Entities;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

namespace Agama.Scripts.Behavior.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Stop Mover", story: "stop [Mover]", category: "Action", id: "d894adca600bf4ccb822b25c89c12919")]
    public partial class StopMoverAction : Action
    {
        [SerializeReference] public BlackboardVariable<EntityMover> Mover;

        protected override Status OnStart()
        {
            Mover.Value.StopImmediately();
            return Status.Success;
        }
    }
}

