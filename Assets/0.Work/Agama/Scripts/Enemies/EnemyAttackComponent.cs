﻿using Agama.Scripts.Core;
using Agama.Scripts.Entities;
using UnityEngine;

namespace Agama.Scripts.Enemies
{
    public class EnemyAttackComponent : EntityAttackComponent
    {
        [SerializeField] private StatSO attackPowerStat;

        private Entity _owner;
        private EntityStat _stat;

        public override void Initialize(Entity owner)
        {
            base.Initialize(owner);

            _owner = owner;
            _stat = _owner.GetComp<EntityStat>();

            damagecaster.InitCaster(owner);

            _owner.GetComp<EntityAnimatorTrigger>().OnAnimationEvent += HandleAnimationEvent;
        }

        protected virtual void HandleAnimationEvent()
        {
            Debug.Log("ss");
            damagecaster.CastDamage(_stat.GetStat(attackPowerStat).BaseValue);
        }

        protected virtual void OnDestroy()
        {
            _owner.GetComp<EntityAnimatorTrigger>().OnAnimationEvent -= HandleAnimationEvent;
        }
    }
}
