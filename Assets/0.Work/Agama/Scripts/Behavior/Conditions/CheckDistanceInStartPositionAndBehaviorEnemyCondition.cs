using Agama.Scripts.Enemies;
using System;
using Unity.Behavior;
using Unity.VisualScripting;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "Check distance in startPosition and BehaviorEnemy", story: "between [BehaviorEnemy] and startPosition [Operator] [Radius]", category: "Conditions", id: "239bcec8ad4210f51ac4e7b9b14aa22b")]
public partial class CheckDistanceInStartPositionAndBehaviorEnemyCondition : Condition
{
    [SerializeReference] public BlackboardVariable<BehaviorEnemy> BehaviorEnemy;
    [Comparison(comparisonType: ComparisonType.All)]
    [SerializeReference] public BlackboardVariable<ConditionOperator> Operator;
    [SerializeReference] public BlackboardVariable<float> Radius;

    public override bool IsTrue()
    {
        float distance = Vector2.Distance(BehaviorEnemy.Value.transform.position, BehaviorEnemy.Value.StartPosition);
        return ConditionUtils.Evaluate(distance, Operator, Radius.Value);
    }
}
