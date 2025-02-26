using Agama.Scripts.Animators;
using Agama.Scripts.Entities;
using Agama.Scripts.Entities.FSM;
using System;
using UnityEngine;

namespace Agama.Scripts.Players.States
{
    public class PlayerHitState : EntityState, IEventState
    {
        public Action OnEventEndEvent { get; set; }

        private Player _player;
        private EntityAnimatorTrigger _animatorTrigger;
        private EntityMover _mover;

        public PlayerHitState(Entity owner, AnimationParamiterSO animationParamitor) : base(owner, animationParamitor)
        {
            _player = owner as Player;
            _animatorTrigger = _player.GetComp<EntityAnimatorTrigger>();
            _mover = _player.GetComp<EntityMover>();
        }

        public override void Enter()
        {
            base.Enter();
            _animatorTrigger.OnAnimationEndEvent += HandleAnimationEndEvent;
            _mover.CanMove = false;
            _mover.StopImmediately();
            _player.SetStateChangeLock(true);
            _player.ClearEventState();
        }

        private void HandleAnimationEndEvent()
        {
            _animatorTrigger.OnAnimationEndEvent -= HandleAnimationEndEvent;
            OnEventEndEvent?.Invoke();
        }

        public override void Exit()
        {
            _mover.CanMove = true;
            _player.SetStateChangeLock(false);
            base.Exit();
        }
    }
}
