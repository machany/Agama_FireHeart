using System;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Agama.Scripts.Core
{
    [CreateAssetMenu(fileName = "PlayerInputSO", menuName = "SO/Input/PlayerInput", order = 0)]
    public class PlayerInputSO : ScriptableObject, Controlls.IPlayerActions, Controlls.IUIActions
    {
        private Controlls _controls;

        private void OnEnable()
        {
            if (_controls == null)
            {
                _controls = new Controlls();
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

        public Action OnAttackPressedEvent;
        public Action OnInteractPressedEvent;
        public Action OnMoveEvent;
        /// <summary>
        /// true = Next(Right), false = Previous(Left)
        /// </summary>
        public Action<bool> OnQuickSlotMoveEvent;
        public Action<byte> OnQuickNumberChanged;

        public Vector2 MoveInputVector { get; private set; }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnAttackPressedEvent?.Invoke();
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnInteractPressedEvent?.Invoke();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            MoveInputVector = context.ReadValue<Vector2>();
            OnMoveEvent?.Invoke();
        }

        public void OnNext(InputAction.CallbackContext context)
        {
            if (context.performed)
            OnQuickSlotMoveEvent?.Invoke(true);
        }

        public void OnPrevious(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnQuickSlotMoveEvent?.Invoke(false);
        }

        public void OnQuickSlot1(InputAction.CallbackContext context)
        {
            if (!context.performed)
                OnQuickNumberChanged?.Invoke(1);
        }

        public void OnQuickSlot2(InputAction.CallbackContext context)
        {
            if (!context.performed)
                OnQuickNumberChanged?.Invoke(2);
        }

        public void OnQuickSlot3(InputAction.CallbackContext context)
        {
            if (!context.performed)
                OnQuickNumberChanged?.Invoke(3);
        }

        public void OnQuickSlot4(InputAction.CallbackContext context)
        {
            if (!context.performed)
                OnQuickNumberChanged?.Invoke(4);
        }

        public void OnQuickSlot5(InputAction.CallbackContext context)
        {
            if (!context.performed)
                OnQuickNumberChanged?.Invoke(5);
        }

#endregion

        #region UI

        public void OnNavigate(InputAction.CallbackContext context) { }
        public void OnSubmit(InputAction.CallbackContext context) { }
        public void OnCancel(InputAction.CallbackContext context) { }
        public void OnPoint(InputAction.CallbackContext context) { }
        public void OnClick(InputAction.CallbackContext context) { }
        public void OnRightClick(InputAction.CallbackContext context) { }
        public void OnMiddleClick(InputAction.CallbackContext context) { }
        public void OnScrollWheel(InputAction.CallbackContext context) { }
        public void OnTrackedDevicePosition(InputAction.CallbackContext context) { }
        public void OnTrackedDeviceOrientation(InputAction.CallbackContext context) { }

        #endregion
    }
}