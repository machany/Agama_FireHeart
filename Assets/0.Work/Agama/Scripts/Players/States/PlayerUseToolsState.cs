using Agama.Scripts.Animators;
using Agama.Scripts.Entities;
using Agama.Scripts.Entities.FSM;
using System;
using UnityEngine;

namespace Agama.Scripts.Players.States
{
    public class PlayerUseToolsState : EntityState, IEventState
    {
        public Action OnEventEndEvent { get; set; }

        private EntityMover _mover;
        private EntityAnimatorTrigger _animatorTrigger;
        private EntityAttackComponent _attackComp;

        public PlayerUseToolsState(Entity owner, AnimationParamiterSO animationParamitor) : base(owner, animationParamitor)
        {
            _mover = _owner.GetComp<EntityMover>();
            _animatorTrigger = _owner.GetComp<EntityAnimatorTrigger>();
            _attackComp = _owner.GetComp<EntityAttackComponent>(true);
        }

        public override void Enter()
        {
            base.Enter();

            _animatorTrigger.OnAnimationEndEvent += HaandleAnimationEndEvent;
            _animatorTrigger.OnAnimationEvent += HaandleOnAnimationEvent;

            _mover.CanMove = false;
            _mover.StopImmediately();
        }

        private void HaandleAnimationEndEvent()
        {
            OnEventEndEvent?.Invoke();
        }

        private void HaandleOnAnimationEvent()
        {
            // 테스트 코드
            Debug.Log("수정 요함.");
            _attackComp.Attack();
        }

        public override void Exit()
        {
            _animatorTrigger.OnAnimationEndEvent -= HaandleAnimationEndEvent;
            _animatorTrigger.OnAnimationEvent -= HaandleOnAnimationEvent;

            _mover.CanMove = true;
            base.Exit();
        }
    }
}
