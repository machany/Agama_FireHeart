using Agama.Scripts.Enemies;
using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "Chack Distance BehaviorEnemy and StartPositon", story: "distance between [BehaviorEnemy] and startPositon [Operator] [Radius]", category: "Conditions", id: "c4028f43f203a64700ed2b810ccba5ea")]
public partial class DistanceBetweenBehaviorEnemyAndStartPositonCondition : Condition
{
    [SerializeReference] public BlackboardVariable<BehaviorEnemy> BehaviorEnemy;
    [Comparison(comparisonType: ComparisonType.All)]
    [SerializeReference] public BlackboardVariable<ConditionOperator> Operator;
    [SerializeReference] public BlackboardVariable<float> Radius;

    public override bool IsTrue()
    {
        if (BehaviorEnemy.Value == null)
            return false;

        float distance = Vector2.Distance(BehaviorEnemy.Value.transform.position, BehaviorEnemy.Value.StartPosition);
        return ConditionUtils.Evaluate(distance, Operator, Radius.Value);
    }
}
