using Agama.Scripts.Animators;
using Agama.Scripts.Entities;
using Agama.Scripts.Entities.FSM;
using System;

namespace Agama.Scripts.Players.States
{
    public class PlayerUseToolsState : EntityState, IEventState
    {
        public Action OnEventEndEvent { get; set; }

        private Player _player;
        private EntityMover _mover;
        private EntityAnimatorTrigger _animatorTrigger;
        private PlayerAttackComponent _attackComp;

        public PlayerUseToolsState(Entity owner, AnimationParamiterSO animationParamitor) : base(owner, animationParamitor)
        {
            _player = _owner as Player;

            _mover = _owner.GetComp<EntityMover>();
            _animatorTrigger = _owner.GetComp<EntityAnimatorTrigger>();
            _attackComp = _owner.GetComp<PlayerAttackComponent>();
            _attackComp = _owner.GetComp<PlayerAttackComponent>();
        }

        public override void Enter()
        {
            base.Enter();

            _player.InputSO.canChangeQuickSlot = false;

            _animatorTrigger.OnAnimationEndEvent += HaandleAnimationEndEvent;
            _animatorTrigger.OnAnimationEvent += HaandleOnAnimationEvent;

            _mover.CanMove = false;
            _mover.StopImmediately();

            _attackComp.UseToolComboChanged();
        }

        private void HaandleAnimationEndEvent()
        {
            OnEventEndEvent?.Invoke();
        }

        private void HaandleOnAnimationEvent()
        {
            _attackComp.Attack();
        }

        public override void Exit()
        {
            _player.InputSO.canChangeQuickSlot = true;

            _animatorTrigger.OnAnimationEndEvent -= HaandleAnimationEndEvent;
            _animatorTrigger.OnAnimationEvent -= HaandleOnAnimationEvent;

            _mover.CanMove = true;
            base.Exit();
        }
    }
}
