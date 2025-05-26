using Runtime.Events;
using Runtime.Extensions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Runtime.Managers
{
    public class InputManager : Monosingleton<InputManager>
    {
        private PlayerInputActions _playerInputActions;

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Enable();

            _playerInputActions.Player.MousePos.performed += OnMousePos;
            _playerInputActions.Player.MouseLClick.started += OnClick;

            InputEvents.Instance.OnEnableInput += OnEnableInput;
            InputEvents.Instance.OnDisableInput += OnDisableInput;
        }
        
        private void OnEnableInput()
        {
            _playerInputActions.Player.Enable();
        }
        
        private void OnDisableInput()
        {
            _playerInputActions.Player.Disable();
        }

        private void OnMousePos(InputAction.CallbackContext obj)
        {
            InputEvents.Instance.OnMouseHover?.Invoke(obj.ReadValue<Vector2>());
        }

        private void OnClick(InputAction.CallbackContext obj)
        {
            InputEvents.Instance.OnMouseClick?.Invoke();
        }
        
        private void UnSubscribeEvents()
        {
            _playerInputActions.Player.MousePos.performed -= OnMousePos;
            _playerInputActions.Player.MouseLClick.started -= OnClick;
            _playerInputActions.Disable();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
        
    }
}