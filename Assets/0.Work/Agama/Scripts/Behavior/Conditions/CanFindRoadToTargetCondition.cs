using Agama.Scripts.Enemies;
using Library;
using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;

namespace Agama.Scripts.Behavior.Conditions
{
    [Serializable, Unity.Properties.GeneratePropertyBag]
    [Condition(name: "Can find road to target", story: "[MoveDirection] set [Target] to find road node [BehaviorEnemy]", category: "Conditions", id: "1026acd7a66f4a24c78b2bbdde4b0dd4")]
    public partial class CanFindRoadToTargetCondition : Condition
    {
        [SerializeReference] public BlackboardVariable<Vector2> MoveDirection;
        [SerializeReference] public BlackboardVariable<Transform> Target;
        [SerializeReference] public BlackboardVariable<BehaviorEnemy> BehaviorEnemy;

        public override bool IsTrue()
        {
            BehaviorEnemy.Value.roadFinder.FindPath(BehaviorEnemy.Value.transform, Target.Value.position);
            Stack<Core.AStar.Node> findedRoad = BehaviorEnemy.Value.roadFinder[BehaviorEnemy.Value.transform]; // 찾은 길의 node를 저장함

            if (findedRoad == null) // 길 못 찾으면 false
                return false;

            if (findedRoad.Count > 0) // 위치가 동일 하지않으면
                MoveDirection.Value = ((Vector2)BehaviorEnemy.Value.transform.position).GetClosestDirection(findedRoad.Pop().worldPosition);
            else
                return false;

            return true;
        }
    }
}