using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

namespace Agama.Scripts.Behavior.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Set Variable null", story: "set [Variable] is null", category: "Action", id: "808febef9f8fce43226c519dd3dd19a5")]
    public partial class SetVariableNullAction : Action
    {
        [SerializeReference] public BlackboardVariable Variable;

        protected override Status OnStart()
        {
            Variable.ObjectValue = null;
            return Status.Success;
        }
    }
}
