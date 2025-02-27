using Agama.Scripts.Animators;
using Agama.Scripts.Entities;
using Agama.Scripts.Entities.FSM;
using System;
using UnityEngine.SceneManagement;

namespace Agama.Scripts.Players.States
{
    public class PlayerDeadState : EntityState, IEventState
    {

        private EntityAnimatorTrigger _trigger;
        public PlayerDeadState(Entity owner, AnimationParamiterSO animationParamitor) : base(owner, animationParamitor)
        {
            _trigger = _owner.GetComp<EntityAnimatorTrigger>();
            _mover = _owner.GetComp<EntityMover>();
        }

        public Action OnEventEndEvent { get; set; }

        private EntityMover _mover;

        public override void Enter()
        {
            base.Enter();
            _trigger.OnAnimationEndEvent += HandleAnimationEnd;
            _mover.CanMove = false;
            _mover.StopImmediately();
            (_owner as Player).SetStateChangeLock(true);
        }

        private void HandleAnimationEnd()
        {
            SceneManager.LoadScene("StartScene");
        }

        public override void Exit()
        {
            _trigger.OnAnimationEndEvent -= HandleAnimationEnd;
            _mover.CanMove = true;
            (_owner as Player).SetStateChangeLock(false);

            base.Exit();
        }
    }
}
