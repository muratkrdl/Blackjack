using Runtime.Events;
using Runtime.Extensions;
using UnityEngine.InputSystem;

namespace Runtime.Managers
{
    public class InputManager : Monosingleton<InputManager>
    {
        private PlayerInputActions _playerInputActions;

        protected override void Awake()
        {
            base.Awake();
            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Enable();

            _playerInputActions.Player.GetNormalCard.started += OnGetNormalCard;
            _playerInputActions.Player.GetSpecialCard.started += OnGetSpecialCard;
        }
        
        private void OnGetNormalCard(InputAction.CallbackContext obj)
        {
            InputEvents.Instance.OnNormalCardDraw?.Invoke();
        }

        private void OnGetSpecialCard(InputAction.CallbackContext obj)
        {
            InputEvents.Instance.OnSpecialCardDraw?.Invoke();
        }
        
    }
}