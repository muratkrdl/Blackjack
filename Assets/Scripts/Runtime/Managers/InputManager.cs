using Runtime.Abstracts.Classes;
using Runtime.Events;
using Runtime.Extensions;
using Runtime.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Runtime.Managers
{
    public class InputManager : Monosingleton<InputManager>
    {
        private CardObject _currentHoveredCard;

        private PlayerInputActions _playerInputActions;

        private Vector3 _gizmosPoint;

        private void OnEnable()
        {
            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Enable();

            _playerInputActions.Player.MousePos.performed += OnMouseHover;
            _playerInputActions.Player.MouseLClick.started += OnClick;
        }

        private void OnMouseHover(InputAction.CallbackContext obj)
        {
            Vector3 mouseScreenPos = obj.ReadValue<Vector2>();
            mouseScreenPos.z = -ConstantsUtilities.MainCamera.transform.position.z;

            Vector3 mouseWorldPos = ConstantsUtilities.MainCamera.ScreenToWorldPoint(mouseScreenPos);
            
            _gizmosPoint = mouseWorldPos;

            Collider2D hit = Physics2D.OverlapPoint(mouseWorldPos, ConstantsUtilities.PlayerCardLayerMask);
            
            if (hit)
            {
                CardObject card = hit.GetComponent<CardObject>();

                if (!card || _currentHoveredCard == card) return;
                
                if (_currentHoveredCard)
                    _currentHoveredCard.OnPointerExit();

                card.OnPointerEnter();
                _currentHoveredCard = card;
            }
            else if (_currentHoveredCard)
            {
                _currentHoveredCard.OnPointerExit();
                _currentHoveredCard = null;
            }
        }

        private void OnClick(InputAction.CallbackContext obj)
        {
            if (_currentHoveredCard)
            {
                // currentHoveredCard.OnClick();
            }
        }

        private void OnDisable()
        {
            _playerInputActions.Player.MousePos.performed -= OnMouseHover;
            _playerInputActions.Player.MouseLClick.started -= OnClick;
            _playerInputActions.Disable();
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_gizmosPoint, 0.1f);
        }
        
    }
}