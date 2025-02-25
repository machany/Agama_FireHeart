using Agama.Scripts.Core;
using Agama.Scripts.Entities;
using Agama.Scripts.Players;
using Scripts.Core.Sound;
using System;
using UnityEngine;

namespace Scripts.Players
{
    public class PlayerSound : EntitySound
    {
        [SerializeField] private SoundSO _attackSound;
        [SerializeField] private SoundSO _walkSound;
        [SerializeField] private SoundSO _swapSound;
        private Player _player;
        private PlayerInputSO _input;
        private int _before;
        public override void Initialize(Entity owner)
        {
            base.Initialize(owner);
            _player = _entity as Player;
            _input = _player.InputSO;
            _input.OnQuickSlotChangedEvent += HandleQuickSlot;
            _anim.OnWalkEvent += HandleWalk;
            _anim.OnAnimationEvent += HandleAttack;
        }
        private void OnDestroy()
        {
            _input.OnQuickSlotChangedEvent -= HandleQuickSlot;
            _anim.OnWalkEvent -= HandleWalk;
            _anim.OnAnimationEvent -= HandleAttack;
        }

        private void HandleQuickSlot(sbyte obj)
        {
            if (_before != obj)
            {
                _before = obj;
                PlaySound(_swapSound);
            }
        }

        private void HandleWalk()
        {
            PlaySound(_walkSound);
        }
        private void HandleAttack()
        {
            PlaySound(_attackSound);
        }
    }
}
