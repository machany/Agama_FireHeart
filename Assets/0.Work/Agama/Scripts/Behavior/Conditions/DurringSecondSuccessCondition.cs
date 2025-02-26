using System;
using Unity.Behavior;
using UnityEngine;

namespace Agama.Scripts.Behavior.Conditions {
    [Serializable, Unity.Properties.GeneratePropertyBag]
    [Condition(name: "Durring Second Success", story: "durring success [Second] with [Under]", category: "Conditions", id: "14a5400ffc21c63b9956051d94b5928f")]
    public partial class DurringSecondSuccessCondition : Condition
    {
        [SerializeReference] public BlackboardVariable<float> Second;
        [SerializeReference] public BlackboardVariable<bool> Under;

        private float _startTime;

        public override bool IsTrue()
        {
            return _startTime + Second.Value >= Time.time != Under.Value;
        }

        public override void OnStart()
        {
            _startTime = Time.time;
        }
    }
}
