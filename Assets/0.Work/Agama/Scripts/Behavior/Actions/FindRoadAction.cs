using Agama.Scripts.Enemies;
using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace Agama.Scripts.Behavior.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Find Road", story: "find road [BehaviorEnemy] to [Target]", category: "Action", id: "482c837ac336c0e0ce5e8e1f369a64ce")]
    public partial class FindRoadAction : Action
    {
        [SerializeReference] public BlackboardVariable<BehaviorEnemy> BehaviorEnemy;
        [SerializeReference] public BlackboardVariable<Transform> Target;

        protected override Status OnStart()
        {
            BehaviorEnemy.Value.roadFinder.FindPath(BehaviorEnemy.Value.transform, Target.Value.transform.position); // 길을 찾기 생성 or 업데이트
            return Status.Success;
        }
    }
}
