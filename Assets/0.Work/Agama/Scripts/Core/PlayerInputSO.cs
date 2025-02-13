using System;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Agama.Scripts.Core
{
    [CreateAssetMenu(fileName = "PlayerInputSO", menuName = "SO/Input/PlayerInput", order = 0)]
    public class PlayerInputSO : ScriptableObject, Controls.IPlayerActions, Controls.IUIActions
    {
        private Controls _controls;

        private void OnEnable()
        {
            if (_controls == null)
            {
                _controls = new Controls();
                _controls.Player.SetCallbacks(this);
                _controls.UI.SetCallbacks(this);
            }
            _controls.Player.Enable();
            _controls.UI.Enable();
        }

        private void OnDisable()
        {
            _controls.Player.Disable();
            _controls.UI.Disable();
        }

        #region Player

        public Action OnItemUseKeyPressedEvent;
        public Action OnInteractKeyPressedEvent;
        public Action OnMoveKeyPressedEvent;
        /// <summary>
        /// true = Next(Right), false = Previous(Left)
        /// </summary>
        public Action<byte> OnQuickSlotChangedEvent;

        [SerializeField] private byte maxQuickSlotCount;

        public Vector2 MoveInputVector { get; private set; }

        public byte CurrentQuickSlotIndex { get; private set; } = 1;

        public void OnItemUse(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnItemUseKeyPressedEvent?.Invoke();
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnInteractKeyPressedEvent?.Invoke();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            MoveInputVector = context.ReadValue<Vector2>();
            if (context.performed)
                OnMoveKeyPressedEvent?.Invoke();
        }

        public void OnNext(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnQuickSlotChangedEvent?.Invoke(CurrentQuickSlotIndex = ++CurrentQuickSlotIndex > maxQuickSlotCount ? CurrentQuickSlotIndex : maxQuickSlotCount);
        }

        public void OnPrevious(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnQuickSlotChangedEvent?.Invoke(CurrentQuickSlotIndex = (byte)(--CurrentQuickSlotIndex < 1 ? 1 : CurrentQuickSlotIndex));
        }

        public void OnQuickSlot1(InputAction.CallbackContext context)
        {
            if (!context.performed)
                OnQuickSlotChangedEvent?.Invoke(CurrentQuickSlotIndex = 1);
        }

        public void OnQuickSlot2(InputAction.CallbackContext context)
        {
            if (!context.performed)
                OnQuickSlotChangedEvent?.Invoke(CurrentQuickSlotIndex = 2);
        }

        public void OnQuickSlot3(InputAction.CallbackContext context)
        {
            if (!context.performed)
                OnQuickSlotChangedEvent?.Invoke(CurrentQuickSlotIndex = 3);
        }

        public void OnQuickSlot4(InputAction.CallbackContext context)
        {
            if (!context.performed)
                OnQuickSlotChangedEvent?.Invoke(CurrentQuickSlotIndex = 4);
        }

        public void OnQuickSlot5(InputAction.CallbackContext context)
        {
            if (!context.performed)
                OnQuickSlotChangedEvent?.Invoke(CurrentQuickSlotIndex = 5);
        }

        #endregion

        #region UI
        public event Action<Vector2> OnScrollWheelEvent;
        public void OnNavigate(InputAction.CallbackContext context) { }
        public void OnSubmit(InputAction.CallbackContext context) { }
        public void OnCancel(InputAction.CallbackContext context) { }
        public void OnPoint(InputAction.CallbackContext context) { }
        public void OnClick(InputAction.CallbackContext context) { }
        public void OnRightClick(InputAction.CallbackContext context) { }
        public void OnMiddleClick(InputAction.CallbackContext context) { }
        public void OnScrollWheel(InputAction.CallbackContext context)
        {
            OnScrollWheelEvent?.Invoke(context.ReadValue<Vector2>());
        }
        public void OnTrackedDevicePosition(InputAction.CallbackContext context) { }
        public void OnTrackedDeviceOrientation(InputAction.CallbackContext context) { }

        #endregion
    }
}