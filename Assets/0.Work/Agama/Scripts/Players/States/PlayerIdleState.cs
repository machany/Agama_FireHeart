﻿using Agama.Scripts.Animators;
using Agama.Scripts.Entities;
using UnityEngine;

namespace Agama.Scripts.Players.States
{
    public class PlayerIdleState : PlayerEventStateUseState
    {
        private EntityMover _mover;

        public PlayerIdleState(Entity owner, AnimationParamiterSO animationParamitor) : base(owner, animationParamitor)
        {
            _mover = owner.GetComp<EntityMover>();
        }

        public override void Enter()
        {
            base.Enter();
            _mover.StopImmediately();
        }

        public override void Update()
        {
            base.Update();
            if (_mover.CanMove && _player.InputSO.MoveInputVector.magnitude > Mathf.Epsilon)
            {
                _player.ChangeState("Player_move_State");
            }
        }
    }
}
