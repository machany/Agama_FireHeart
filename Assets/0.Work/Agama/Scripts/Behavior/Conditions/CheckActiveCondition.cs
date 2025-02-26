using System;
using Unity.Behavior;
using UnityEngine;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "Check Active", story: "[Target] is active [CheckBoolean]", category: "Conditions", id: "3caa1460130dd0085aecbd150141b8ef")]
public partial class CheckActiveCondition : Condition
{
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<bool> CheckBoolean;

    public override bool IsTrue()
    {
        return Target.Value.activeSelf == CheckBoolean.Value;
    }
}
