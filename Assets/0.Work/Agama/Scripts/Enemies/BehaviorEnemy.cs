using Agama.Scripts.Entities;
using Scripts.Feedbacks;
using UnityEngine;

namespace Agama.Scripts.Enemies
{
    public abstract class BehaviorEnemy : Entity
    {
        [SerializeField] private DropFeedback _dropFeedback;

        protected override void Awake()
        {
            base.Awake();

            if (_dropFeedback == null)
                transform.GetComponentInChildren<DropFeedback>();
        }

        protected override void HandleDeadEvent()
        {
            _dropFeedback.CreateFeedback();
        }
    }
}