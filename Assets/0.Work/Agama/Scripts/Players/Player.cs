using Agama.Scripts.Animators;
using Agama.Scripts.Core;
using Agama.Scripts.Entities;
using Agama.Scripts.Entities.FSM;
using Scripts.Core;
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

        public Action<sbyte, int> OnToolTypeChanged;

        public sbyte ToolType { get; private set; }
        public bool StateChangeLock { get; private set; }

        private EntityStateMachine _stateMachine;
        private EntityRenderer _renderer;

        protected override void Awake()
        {
            base.Awake();
            _stateMachine = new EntityStateMachine(this, stateList, maxEventStateStorageCount);

            InputSO.OnItemUseKeyPressedEvent += HandleItemUseKeyPressedEvent;
            InputSO.OnQuickSlotChangedEvent += HandleQuickSlotChangedEvent;
            StateChangeLock = false;

            _renderer = GetComp<EntityRenderer>();
        }

        protected override void OnDestroy()
        {
            InputSO.OnItemUseKeyPressedEvent -= HandleItemUseKeyPressedEvent;
            InputSO.OnQuickSlotChangedEvent -= HandleQuickSlotChangedEvent;
            _stateMachine.DestoryObject();

            base.OnDestroy();
        }

        private void Update()
        {
            _stateMachine.UpdateState();
        }

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

        private void HandleItemUseKeyPressedEvent()
        {
            // ������ ���� �� ������� ����
            ChangeState("Player_use_tool_State_event");
        }

        private void HandleQuickSlotChangedEvent(sbyte value)
        {
            // ��� �ִ� ������ ���� �� toolType����

            ToolType = value;
            _renderer.SetParamiter(ToolTypeParam, ToolType);

            // �� ������ ���ݷ��� ������ ��. �� ��� ���� ���� ��õ
            OnToolTypeChanged?.Invoke(ToolType, 1);
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
    }
}