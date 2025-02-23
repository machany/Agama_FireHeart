using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

namespace Agama.Scripts.Behavior.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Set Vector2 Random", story: "set [Direction] random in between [MinValue] and [MaxValue]", category: "Action", id: "cd3ceda4efdc32034c14102e5ac1ed4d")]
    public partial class SetVector2RandomAction : Action
    {
        [SerializeReference] public BlackboardVariable<Vector2> Direction;
        [SerializeReference] public BlackboardVariable<float> MinValue;
        [SerializeReference] public BlackboardVariable<float> MaxValue;

        protected override Status OnStart()
        {
            Direction.Value = new Vector2(UnityEngine.Random.Range(MinValue.Value, MaxValue.Value), UnityEngine.Random.Range(MinValue.Value, MaxValue.Value));
            return Status.Success;
        }
    }
}