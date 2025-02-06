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
            _player.InputSO.OnItemUseKeyPressedEvent += HandleItemUseKeyPressedEvent;
            _player.InputSO.OnInteractKeyPressedEvent += HandleInteractKeyPressedEvent;
        }

        public override void Exit()
        {
            _player.InputSO.OnItemUseKeyPressedEvent -= HandleItemUseKeyPressedEvent;
            _player.InputSO.OnInteractKeyPressedEvent -= HandleInteractKeyPressedEvent;
            base.Exit();
        }

        protected virtual void HandleQuickSlotItemChangedEvent()
        {
            // 1. 이벤트 체널
            // 2. 걍 PlayerInputSO
            // _player.ChangeState("Player_use_item_State_event");
        }

        protected virtual void HandleItemUseKeyPressedEvent()
        {
            // 이걸 어찌 처리 해야할까
            // 퀵슬롯의 들고 있는 아이템 따라서 변경 ㄱㄱ
        }

        protected virtual void HandleInteractKeyPressedEvent()
        {
            // 감지된 얘가 상호작용한지 체크 후 상태 변경 불가능 시 그냥 리턴
            _player.SetStateChangeLock(!_player.StateChangeLock);
            _player.ChangeState("Player_idle_State");
            // 생각이 좀 필요 할듯

            // 문제 => 인터렉트 중 (제작창 등이 나옴) 처리를 어떻게 할 것인가?
            // 방안
            // 1. Idle상태에서 Lock
            // 2. Interact상태를 만들어 따로 처리
            // 3. Player에서 Idle로 바꾸고 ChangeState를 Lock
            // 예상 장점
            // 1 -> 3번의 문제인 ChangeState외의 동작을 막을 수 있음
            // 2 -> 관리가 편함
            // 3 -> 코드의 구조 및 가독성이 매우 좋고, 가독성을 해치지 않음
            // 예상 단점
            // 1 -> IdleState의 코드의 가독 가시성 떨어짐
            // 2 -> 코드 구조의 복잡성 증가
            // 3 -> IdleState의 ChangeState외의 동작을 막지 못함
        }
    }
}
