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

        public PlayerHitState(Entity owner, AnimationParamitorSO animationParamitor) : base(owner, animationParamitor)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _owner.GetComp<EntityAnimatorTrigger>().OnAnimationEndEvent += HandleAnimationEndEvent;
            _owner.GetComp<EntityMover>().CanMove = false;
        }

        private void HandleAnimationEndEvent()
        {
            _owner.GetComp<EntityAnimatorTrigger>().OnAnimationEndEvent -= HandleAnimationEndEvent;
            OnEventEndEvent?.Invoke();
        }

        public override void Exit()
        {
            _owner.GetComp<EntityMover>().CanMove = true;
            base.Exit();
        }
    }
}
