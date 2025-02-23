using Agama.Scripts.Entities;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

namespace Agama.Scripts.Behavior.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Set Mover Speed", story: "[Mover] set [SpeedMultiplier]", category: "Action", id: "94c5e74cafeadca78cb27d1f11e4fcee")]
    public partial class SetMoverSpeedAction : Action
    {
        [SerializeReference] public BlackboardVariable<EntityMover> Mover;
        [SerializeReference] public BlackboardVariable<float> SpeedMultiplier;

        protected override Status OnStart()
        {
            Mover.Value.SetMoveSpeedMultiplier(SpeedMultiplier.Value);
            return Status.Success;
        }
    }
}
