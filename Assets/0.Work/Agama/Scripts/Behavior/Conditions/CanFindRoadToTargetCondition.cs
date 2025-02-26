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
            Stack<Core.AStar.Node> findedRoad = BehaviorEnemy.Value.roadFinder[BehaviorEnemy.Value.transform]; // ã�� ���� node�� ������

            if (findedRoad == null) // �� �� ã���� false
                return false;

            if (findedRoad.Count > 0) // ��ġ�� ���� ����������
                MoveDirection.Value = ((Vector2)BehaviorEnemy.Value.transform.position).GetClosestDirection(findedRoad.Pop().worldPosition);
            else
                return false;

            return true;
        }
    }
}