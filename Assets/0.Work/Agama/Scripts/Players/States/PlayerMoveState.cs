using Agama.Scripts.Animators;
using Agama.Scripts.Entities;
using UnityEngine;

namespace Agama.Scripts.Players.States
{
    public class PlayerMoveState : PlayerEventStateUseState
    {
        private EntityMover _mover;

        public PlayerMoveState(Entity owner, AnimationParamitorSO animationParamitor) : base(owner, animationParamitor)
        {
            _mover = owner.GetComp<EntityMover>();
        }

        public override void Update()
        {
            base.Update();
            Vector2 inputValue = _player.InputSO.MoveInputVector.normalized;

            _mover.SetMovement(inputValue);

            if (inputValue.magnitude < Mathf.Epsilon)
                _player.ChangeState("Player_idle_State");
        }
    }
}
