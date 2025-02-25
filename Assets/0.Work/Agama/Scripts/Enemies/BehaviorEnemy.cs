using Agama.Scripts.Core.AStar;
using Agama.Scripts.Entities;
using Scripts.Feedbacks;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;

namespace Agama.Scripts.Enemies
{
    public abstract class BehaviorEnemy : Entity
    {
        [BlackboardEnum]
        public enum BehaviorEnemyState
        {
            Patroll,
            Chase,
            Attack,
            Hit,
            Dead
        }


        [SerializeField] protected DropFeedback dropFeedback;

        public RoadFinder roadFinder;
        public LayerMask targetLayer;
        public Vector2 StartPosition { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            if (dropFeedback == null)
                transform.GetComponentInChildren<DropFeedback>();

            StartPosition = transform.position;
        }

        protected override void HandleDeadEvent()
        {
            dropFeedback.CreateFeedback();
        }
    }
}