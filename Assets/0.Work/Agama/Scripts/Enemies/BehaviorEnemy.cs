using Agama.Scripts.Entities;
using Scripts.Feedbacks;
using UnityEngine;

namespace Agama.Scripts.Enemies
{
    public abstract class BehaviorEnemy : Entity
    {
        [SerializeField] protected DropFeedback _dropFeedback;

        public Vector2 StartPosition { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            if (_dropFeedback == null)
                transform.GetComponentInChildren<DropFeedback>();

            StartPosition = transform.position;
        }

        protected override void HandleDeadEvent()
        {
            _dropFeedback.CreateFeedback();
        }
    }
}