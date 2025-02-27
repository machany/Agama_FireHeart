using Agama.Scripts.Animators;
using Agama.Scripts.Entities;
using Agama.Scripts.Entities.FSM;
using System;

namespace Agama.Scripts.Players.States
{
    public class PlayerDeadState : EntityState, IEventState
    {
        public PlayerDeadState(Entity owner, AnimationParamiterSO animationParamitor) : base(owner, animationParamitor)
        {
            _mover = _owner.GetComp<EntityMover>();
        }

        public Action OnEventEndEvent { get; set; }

        private EntityMover _mover;

        public override void Enter()
        {
            base.Enter();
            _mover.CanMove = false;
            _mover.StopImmediately();
            (_owner as Player).SetStateChangeLock(true);
        }

        public override void Exit()
        {
            _mover.CanMove = true;
            (_owner as Player).SetStateChangeLock(false);

            base.Exit();
        }
    }
}
