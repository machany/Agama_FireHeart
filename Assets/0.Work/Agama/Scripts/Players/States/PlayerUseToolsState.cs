using Agama.Scripts.Animators;
using Agama.Scripts.Entities;
using Agama.Scripts.Entities.FSM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Agama.Scripts.Players.States
{
    public class PlayerUseToolsState : EntityState, IEventState
    {
        public Action OnEventEndEvent { get; set; }

        private EntityMover _mover;
        private EntityAnimatorTrigger _animatorTrigger;

        public PlayerUseToolsState(Entity owner, AnimationParamiterSO animationParamitor) : base(owner, animationParamitor)
        {
            _mover = owner.GetComp<EntityMover>();
            _animatorTrigger = owner.GetComp<EntityAnimatorTrigger>();
        }

        public override void Enter()
        {
            base.Enter();
            _animatorTrigger.OnAnimationEndEvent += HaandleAnimationEndEvent;
            Debug.Log("Add Event");
            _mover.CanMove = false;
            _mover.StopImmediately();
        }

        private void HaandleAnimationEndEvent()
        {
            _animatorTrigger.OnAnimationEndEvent -= HaandleAnimationEndEvent;
            Debug.Log("Remove Event");
            OnEventEndEvent?.Invoke();
        }

        public override void Exit()
        {
            _mover.CanMove = true;
            base.Exit();
        }
    }
}
