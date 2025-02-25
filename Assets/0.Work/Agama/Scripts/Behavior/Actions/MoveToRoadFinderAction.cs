using Agama.Scripts.Enemies;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using Agama.Scripts.Entities;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Move To RoadFinder", story: "[Mover] move to [Target] follow road [BehaviorEnemy]", category: "Action", id: "1393d15346f39f1c12a024010ee3680d")]
public partial class MoveToRoadFinderAction : Action
{
    [SerializeReference] public BlackboardVariable<EntityMover> Mover;
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<BehaviorEnemy> BehaviorEnemy;

    private bool _arriveFlag;

    protected override Status OnStart()
    {
        BehaviorEnemy.Value.roadFinder.FindPath(BehaviorEnemy.Value.transform, Target.Value.position);
        Mover.Value.SetMoveFor(BehaviorEnemy.Value.roadFinder[BehaviorEnemy.Value.transform].Pop().worldPosition, () => _arriveFlag = true);
        _arriveFlag = false;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (_arriveFlag)
            return Status.Success;

        return Status.Running;
    }
}
