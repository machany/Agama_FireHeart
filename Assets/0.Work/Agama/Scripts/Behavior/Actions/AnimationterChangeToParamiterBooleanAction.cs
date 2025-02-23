using Agama.Scripts.Entities;
using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

namespace Agama.Scripts.Behavior.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Animationter Change to Paramiter Boolean", story: "[Renderer] change to [Paramiter] [Value]", category: "Action", id: "a2d100f8553cade19e1cb632a856ce55")]
    public partial class AnimationterChangeToParamiterBooleanAction : Action
    {
        [SerializeReference] public BlackboardVariable<EntityRenderer> Renderer;
        [SerializeReference] public BlackboardVariable<string> Paramiter;
        [SerializeReference] public BlackboardVariable<bool> Value;

        protected override Status OnStart()
        {
            Renderer.Value.AnimatorComp.SetBool(Paramiter.Value, Value.Value);
            return Status.Success;
        }
    }
}
