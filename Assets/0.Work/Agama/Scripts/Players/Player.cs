using Agama.Scripts.Animators;
using Agama.Scripts.Combats;
using Agama.Scripts.Core;
using Agama.Scripts.Entities;
using Agama.Scripts.Entities.FSM;
using System;
using UnityEngine;

namespace Agama.Scripts.Players
{
    public class Player : Entity
    {
        [field: SerializeField] public PlayerInputSO InputSO { get; private set; }
        [field: SerializeField] public AnimationParamiterSO CarryParam { get; private set; }
        [field: SerializeField] public AnimationParamiterSO ToolTypeParam { get; private set; }

        [SerializeField] private EntityStateSOList stateList;

        [Tooltip("<color=red>대기 상태</color>로 남아 있을 수 있는 최대 수입니다.\n에를 들어, 값이 3이라면 <color=green>(현재 진행중인 이벤트 스테이트) + (보관중인 이벤트 스테이트){최대 용량 3}</color>으로 4개의 이벤트를 처리하는 것으로 <color=red>보일 수 있습니다.</color>")]
        [SerializeField] private int maxEventStateStorageCount = 3;

        public Action OnUseItem;
        public Action<sbyte, float> OnQuickSloatItemChange;

        public sbyte ToolType { get; private set; }
        public bool StateChangeLock { get; private set; }

        private EntityStateMachine _stateMachine;
        private EntityRenderer _renderer;

        private bool _carring;

        protected override void Awake()
        {
            base.Awake();
            _stateMachine = new EntityStateMachine(this, stateList, maxEventStateStorageCount);

            InputSO.OnItemUseKeyPressedEvent += HandleItemUseKeyPressedEvent;

            StateChangeLock = false;

            _renderer = GetComp<EntityRenderer>();
        }

        protected override void OnDestroy()
        {
            InputSO.OnItemUseKeyPressedEvent -= HandleItemUseKeyPressedEvent;
            _stateMachine.DestoryObject();

            base.OnDestroy();
        }

        private void Update()
        {
            _stateMachine.UpdateState();
        }

        public void ClearEventState()
            => _stateMachine.ClearEventState();

        /// <summary>
        /// 명명 규칙 : 반드시 스테이트 이름은 영문 소문자로 입력하며, 띄어쓰기의 기준은 다른 단어를 사용할 때를 기준으로 언더바(_)를 사용한다.
        /// </summary>
        /// <remarks>
        /// 규격 : 해당 스테이트를 사용하는 (entity의 이름, &quot;첫 알파벳만 대문자&quot;)이 가장 먼저 앞으로 와야하며, 마지막에 (State_스테이트의 타입)을 붙인다. (event, default등) 단, default는 생략할 수 있다. [예 : &quot;P&quot;layer_use_item_&quot;S&quot;tate_event]
        /// </remarks>
        public void ChangeState(string stateName)
        {
            if (StateChangeLock)
                return;

            _stateMachine.ChangeState(stateName);
        }

        public void SetStateChangeLock(bool value)
            => StateChangeLock = value;

        public void ChangeQuickSlotItem(sbyte damageType, float power)
        {
            bool isCarryItem = _carring = damageType < -1;
            ToolType = damageType;
            _renderer.SetParamiter(CarryParam, isCarryItem); // 양수면 안 드는 물건
            _renderer.SetParamiter(ToolTypeParam,(sbyte)Mathf.Abs(damageType));
            OnQuickSloatItemChange?.Invoke(damageType, power);
        }

        private void HandleItemUseKeyPressedEvent()
        {
            Debug.Log(_carring);
            Debug.Log(ToolType);
            if (!_carring && ToolType >= 0)
                ChangeState("Player_use_tool_State_event");
            else
                OnUseItem?.Invoke();
        }

        protected override void HandleHitEvent()
        {
            ChangeState("Player_hit_State_event");
        }

        protected override void HandleDeadEvent()
        {
            // 즉시 변경
            _stateMachine.ChangeState("Player_dead_State_event");
        }

        public override void ApplyDamage(DamageMethodType damageType, float damage, Entity dealer)
        {
            OnDamage?.Invoke(damage);
        }
    }
}