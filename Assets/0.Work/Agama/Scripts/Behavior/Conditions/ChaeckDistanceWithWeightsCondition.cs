using System;
using Unity.Behavior;
using UnityEngine;

namespace Agama.Scripts.Behavior.Conditions
{
    [Serializable, Unity.Properties.GeneratePropertyBag]
    [Condition(name: "Chaeck distance with weights", story: "distance between [Transform] and [Target] with [Weights] [Operator] [Range]", category: "Conditions", id: "239b7b54ea5d23b4aa23597d10270af2")]
    public partial class ChaeckDistanceWithWeightsCondition : Condition
    {
        [SerializeReference] public BlackboardVariable<Transform> Transform;
        [SerializeReference] public BlackboardVariable<Transform> Target;
        [SerializeReference] public BlackboardVariable<Vector3> Weights;
        [Comparison(comparisonType: ComparisonType.All)]
        [SerializeReference] public BlackboardVariable<ConditionOperator> Operator;
        [SerializeReference] public BlackboardVariable<float> Range;

        public override bool IsTrue()
        {
            Vector3 diff = Target.Value.position - Transform.Value.position;

            bool xOperat = ConditionUtils.Evaluate(Mathf.Abs(diff.x), Operator, Mathf.Abs(Range.Value * Weights.Value.x));
            bool yOperat = ConditionUtils.Evaluate(Mathf.Abs(diff.y), Operator, Mathf.Abs(Range.Value * Weights.Value.y));
            bool zOperat = ConditionUtils.Evaluate(Mathf.Abs(diff.z), Operator, Mathf.Abs(Range.Value * Weights.Value.z));

            return xOperat && yOperat && zOperat;
        }
    }
}