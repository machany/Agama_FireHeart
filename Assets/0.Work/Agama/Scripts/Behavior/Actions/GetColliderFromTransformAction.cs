using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace Agama.Scripts.Behavior.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Get Collider from Transform", story: "get [Collider] from [Transform]", category: "Action", id: "ad1832e3e3b7390aafd91938ef8ddeb3")]
    public partial class GetColliderFromTransformAction : Action
    {
        [SerializeReference] public BlackboardVariable<Collider2D> Collider;
        [SerializeReference] public BlackboardVariable<Transform> Transform;

        protected override Status OnStart()
        {
            if (Transform.Value.TryGetComponent<Collider2D>(out Collider2D collider))
            {
                Collider.Value = collider;
            }
            else
            {
                return Status.Failure;
            }

            return Status.Success;
        }
    }
}
