using Agama.Scripts.Core.AStar;
using Agama.Scripts.Entities;
using GGMPool;
using Scripts.Feedbacks;
using Scripts.GameSystem;
using System;
using Unity.Behavior;
using UnityEngine;

namespace Agama.Scripts.Enemies
{
    public abstract class BehaviorEnemy : Entity,IPoolable
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

        public Action<Transform> OnFindTarget;
        public RoadFinderSO roadFinder;

        public LayerMask targetLayer;
        public Vector2 StartPosition { get; private set; }
        [SerializeField] private PoolTypeSO _myType;
        public PoolTypeSO PoolType => _myType;

        public GameObject GameObject => gameObject;
        private Pool _myPool;

        protected override void Awake()
        {
            base.Awake();

            if (dropFeedback == null)
                transform.GetComponentInChildren<DropFeedback>();

            StartPosition = transform.position;
        }
        private void Start()
        {
            TimeManager.Instance.IsNight.OnValueChanged += HandleIsNight;
        }

        private void HandleIsNight(bool prev, bool next)
        {
            if (prev)
                _myPool.Push(this);
        }

        protected override void HandleDeadEvent()
        {
            dropFeedback.CreateFeedback();
            _myPool.Push(this);
        }

        protected override void HandleHitEvent()
        {
        }

        public void SetUpPool(Pool pool)
        {
            _myPool = pool;
        }

        public void ResetItem()
        {
            OnResetEvent?.Invoke();
        }
    }
}