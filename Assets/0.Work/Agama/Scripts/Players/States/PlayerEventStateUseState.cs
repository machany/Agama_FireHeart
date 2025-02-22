using Agama.Scripts.Animators;
using Agama.Scripts.Entities;
using Agama.Scripts.Entities.FSM;
using UnityEngine;

namespace Agama.Scripts.Players.States
{
    public abstract class PlayerEventStateUseState : EntityState
    {
        protected Player _player;

        public PlayerEventStateUseState(Entity owner, AnimationParamiterSO animationParamitor) : base(owner, animationParamitor)
        {
            _player = owner as Player;
            Debug.Assert(_player != null, $"{this.GetType()} : player is null");
        }

        public override void Enter()
        {
            base.Enter();
            _player.InputSO.OnInteractKeyPressedEvent += HandleInteractKeyPressedEvent;
        }

        public override void Exit()
        {
            _player.InputSO.OnInteractKeyPressedEvent -= HandleInteractKeyPressedEvent;
            base.Exit();
        }

        protected virtual void HandleInteractKeyPressedEvent()
        {
            _player.ChangeState("Player_idle_State");
        }
    }
}
