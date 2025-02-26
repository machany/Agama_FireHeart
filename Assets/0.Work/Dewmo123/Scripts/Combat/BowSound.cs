using Agama.Scripts.Entities;
using Scripts.Core.Sound;
using Scripts.Players;
using Scripts.Structures;
using System;
using UnityEngine;

namespace Scripts.Combat
{
    public class BowSound : EntitySound
    {
        [SerializeField] private SoundSO _attackSound;
        public override void Initialize(Entity owner)
        {
            base.Initialize(owner);
            _entity.GetComp<EntityAnimatorTrigger>().OnAnimationEvent += HandleAttackTrigger;
        }
        private void OnDestroy()
        {
            _entity.GetComp<EntityAnimatorTrigger>().OnAnimationEvent -= HandleAttackTrigger;
        }
        private void HandleAttackTrigger()
        {
            PlaySound(_attackSound);
        }
    }
}
