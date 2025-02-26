using Agama.Scripts.Core;
using Agama.Scripts.Entities;
using GGMPool;
using Scripts.Combat;
using Unity.Behavior;
using UnityEngine;

namespace Agama.Scripts.Enemies
{
    public class BomberGoblinAttackComponent : EntityAttackComponent
    {
        [Header("Projectile")]
        [SerializeField] private Transform bombSpawnTransform;
        [SerializeField] private PoolManagerSO poolManager;
        [SerializeField] private PoolTypeSO dynamate;

        [Header("Other")]
        [SerializeField] private StatSO attackPowerStat;

        private Entity _owner;
        private Transform _target;
        private EntityStat _stat;

        public override void Initialize(Entity owner)
        {
            base.Initialize(owner);

            _owner = owner;
            _stat = _owner.GetComp<EntityStat>();

            _owner.GetComp<EntityAnimatorTrigger>().OnAnimationEvent += HandleAnimationEvent;
            (_owner as BehaviorEnemy).OnFindTarget = HandleFindTarget;
        }

        private void HandleFindTarget(Transform target)
        {
            _target = target;
        }

        protected virtual void HandleAnimationEvent()
        {
            Projectile projectile = poolManager.Pop(dynamate).GameObject.GetComponent<Projectile>();
            projectile.SetDamage(_stat.GetStat(attackPowerStat).BaseValue);
            projectile.Init(_target.position - _owner.transform.position, bombSpawnTransform.position);
        }

        protected virtual void OnDestroy()
        {
            _owner.GetComp<EntityAnimatorTrigger>().OnAnimationEvent -= HandleAnimationEvent;
        }
    }
}