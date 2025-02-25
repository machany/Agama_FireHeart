using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "Check Variable is Null", story: "[Variable] is null [CheckBoolean]", category: "Conditions", id: "75da87faf7b6ca8b50b5e927ffbaf2d4")]
public partial class CheckVariableIsNullCondition : Condition
{
    [SerializeReference] public BlackboardVariable<Transform> Variable;
    [SerializeReference] public BlackboardVariable<bool> CheckBoolean;

    public override bool IsTrue()
    {
        return Variable.ObjectValue == null && CheckBoolean.Value;
    }
}
