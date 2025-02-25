using Agama.Scripts.Enemies;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Set Target if in radius", story: "set [Target] in [Radius] with layer [BehaviorEnemy]", category: "Action", id: "5068af1c77e1552456f52781276001da")]
public partial class SetTargetIfInRadiusAction : Action
{
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<float> Radius;
    [SerializeReference] public BlackboardVariable<BehaviorEnemy> BehaviorEnemy;

    protected override Status OnStart()
    {
        Collider2D overlap = Physics2D.OverlapCircle(BehaviorEnemy.Value.transform.position, Radius.Value, BehaviorEnemy.Value.targetLayer);

        if (overlap)
            Target.Value = overlap.transform;

        return Status.Success;
    }
}

