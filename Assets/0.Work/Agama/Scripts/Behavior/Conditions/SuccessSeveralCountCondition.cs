using System;
using Unity.Behavior;
using UnityEngine;

namespace Agama.Scripts.Behavior.Conditions
{
    [Serializable, Unity.Properties.GeneratePropertyBag]
    [Condition(name: "Success Several Count", story: "sucees several [Count]", category: "Conditions", id: "837b75b8683fc72efaa295d077343849")]
    public partial class SuccessSeveralCountCondition : Condition
    {
        [SerializeReference] public BlackboardVariable<int> Count;

        private int _count;

        public override bool IsTrue()
        {
            return Count.Value > _count++;
        }

        public override void OnStart()
        {
            _count = 0;
        }

        public override void OnEnd()
        {
        }
    }
}
