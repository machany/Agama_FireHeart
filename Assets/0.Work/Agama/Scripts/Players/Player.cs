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

        [Tooltip("<color=red>��� ����</color>�� ���� ���� �� �ִ� �ִ� ���Դϴ�.\n���� ���, ���� 3�̶�� <color=green>(���� �������� �̺�Ʈ ������Ʈ) + (�������� �̺�Ʈ ������Ʈ){�ִ� �뷮 3}</color>���� 4���� �̺�Ʈ�� ó���ϴ� ������ <color=red>���� �� �ֽ��ϴ�.</color>")]
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
        /// ��� ��Ģ : �ݵ�� ������Ʈ �̸��� ���� �ҹ��ڷ� �Է��ϸ�, ������ ������ �ٸ� �ܾ ����� ���� �������� �����(_)�� ����Ѵ�.
        /// </summary>
        /// <remarks>
        /// �԰� : �ش� ������Ʈ�� ����ϴ� (entity�� �̸�, &quot;ù ���ĺ��� �빮��&quot;)�� ���� ���� ������ �;��ϸ�, �������� (State_������Ʈ�� Ÿ��)�� ���δ�. (event, default��) ��, default�� ������ �� �ִ�. [�� : &quot;P&quot;layer_use_item_&quot;S&quot;tate_event]
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
            _renderer.SetParamiter(CarryParam, isCarryItem); // ����� �� ��� ����
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
            // ��� ����
            _stateMachine.ChangeState("Player_dead_State_event");
        }

        public override void ApplyDamage(DamageMethodType damageType, float damage, Entity dealer)
        {
            OnDamage?.Invoke(damage);
        }
    }
}