using System;
using Unity.Behavior;
using UnityEngine;

namespace Agama.Scripts.Behavior.Conditions {
    [Serializable, Unity.Properties.GeneratePropertyBag]
    [Condition(name: "Durring Second Success", story: "durring success [Second]", category: "Conditions", id: "14a5400ffc21c63b9956051d94b5928f")]
    public partial class DurringSecondSuccessCondition : Condition
    {
        [SerializeReference] public BlackboardVariable<float> Second;

        private float _startTime;

        public override bool IsTrue()
        {
            return _startTime + Second.Value >= Time.time;
        }

        public override void OnStart()
        {
            _startTime = Time.time;
        }

        public override void OnEnd()
        {
        }
    }
}
