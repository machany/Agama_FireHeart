using System;
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
        public void DisablePCInput()
        {
            _controls.Player.Disable();
        }
        public void EnablePCInput()
        {
            _controls.Player.Enable();
        }
        #region Player

        public Action OnItemUseKeyPressedEvent;
        public Action OnInteractKeyPressedEvent;
        public Action OnMoveKeyPressedEvent;
        public Action OnInventoryKeyPressed;
        /// <summary>
        /// true = Next(Right), false = Previous(Left)
        /// </summary>
        public Action<sbyte> OnQuickSlotChangedEvent;

        [SerializeField] private sbyte maxQuickSlotCount;

        public Vector2 MoveInputVector { get; private set; }
        public Vector2 PreviousInputVector { get; private set; }

        public bool canChangeQuickSlot = true;
        public sbyte CurrentQuickSlotIndex { get; private set; } = 1;

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
            {
                OnMoveKeyPressedEvent?.Invoke();
                PreviousInputVector = context.ReadValue<Vector2>();
            }
        }

        public void OnNext(InputAction.CallbackContext context)
        {
            if (context.performed)
                if (canChangeQuickSlot)
                    OnQuickSlotChangedEvent?.Invoke(CurrentQuickSlotIndex = (sbyte)Mathf.Min(++CurrentQuickSlotIndex, maxQuickSlotCount));
        }

        public void OnPrevious(InputAction.CallbackContext context)
        {
            if (context.performed)
                if (canChangeQuickSlot)
                    OnQuickSlotChangedEvent?.Invoke(CurrentQuickSlotIndex = (sbyte)Mathf.Max(--CurrentQuickSlotIndex, 0));
        }

        public void OnQuickSlot1(InputAction.CallbackContext context)
        {
            if (context.performed)
                if (canChangeQuickSlot)
                    OnQuickSlotChangedEvent?.Invoke(CurrentQuickSlotIndex = 0);
        }

        public void OnQuickSlot2(InputAction.CallbackContext context)
        {
            if (context.performed)
                if (canChangeQuickSlot)
                    OnQuickSlotChangedEvent?.Invoke(CurrentQuickSlotIndex = 1);
        }

        public void OnQuickSlot3(InputAction.CallbackContext context)
        {
            if (context.performed)
                if (canChangeQuickSlot)
                    OnQuickSlotChangedEvent?.Invoke(CurrentQuickSlotIndex = 2);
        }

        public void OnQuickSlot4(InputAction.CallbackContext context)
        {
            if (context.performed)
                if (canChangeQuickSlot)
                    OnQuickSlotChangedEvent?.Invoke(CurrentQuickSlotIndex = 3);
        }

        public void OnQuickSlot5(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnQuickSlotChangedEvent?.Invoke(CurrentQuickSlotIndex = 4);
        }
        public void OnInventory(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnInventoryKeyPressed?.Invoke();
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
            if (canChangeQuickSlot)
                OnQuickSlotChangedEvent?.Invoke(CurrentQuickSlotIndex = (sbyte)Mathf.Clamp(CurrentQuickSlotIndex - (sbyte)context.ReadValue<Vector2>().y, 0, maxQuickSlotCount));
        }
        public void OnTrackedDevicePosition(InputAction.CallbackContext context) { }
        public void OnTrackedDeviceOrientation(InputAction.CallbackContext context) { }


        #endregion
    }
}