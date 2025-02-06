using Agama.Scripts.Animators;
using Agama.Scripts.Core;
using Agama.Scripts.Entities;
using Agama.Scripts.Entities.FSM;
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

        private EntityStateMachine _stateMachine;

        public bool StateChangeLock { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            _stateMachine = new EntityStateMachine(this, stateList, maxEventStateStorageCount);

            InputSO.OnItemUseKeyPressedEvent += HandleItemUseKeyPressedEvent;
            InputSO.OnQuickSlotChangedEvent += HandleQuickSlotChangedEvent;
            StateChangeLock = false;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            InputSO.OnItemUseKeyPressedEvent -= HandleItemUseKeyPressedEvent;
            _stateMachine.DestoryObject();
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

        private void HandleQuickSlotChangedEvent()
        {

        }

        protected override void HandleHitEvent()
        {
            ChangeState("Player_hit_State_event");
        }

        protected override void HandleDeadEvent()
        {
            _stateMachine.ChangeState("Player_dead_State_event");
        }
    }
}