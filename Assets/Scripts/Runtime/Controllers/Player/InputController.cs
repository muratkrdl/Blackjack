using Runtime.Abstracts.Classes;
using Runtime.Events;
using Runtime.Utilities;
using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class InputController : MonoBehaviour
    {
        private CardObject _currentHoveredCard;

        private Vector3 _mouseScreenPos;
        private Vector3 _gizmosPoint;
        
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputEvents.Instance.OnMouseHover += OnMouseHover;
            InputEvents.Instance.OnMouseClick += OnMouseClick;
        }

        private void OnMouseHover(Vector2 value)
        {
            _mouseScreenPos = value;
            _mouseScreenPos.z = -ConstantsUtilities.MainCamera.transform.position.z;
        }
        
        private void OnMouseClick()
        {
            if (_currentHoveredCard)
            {
                // _currentHoveredCard.OnClick();
            }
        }

        private void Update()
        {
            Vector3 mouseWorldPos = ConstantsUtilities.MainCamera.ScreenToWorldPoint(_mouseScreenPos);

            Collider2D hit = Physics2D.OverlapPoint(mouseWorldPos, ConstantsUtilities.InteractableLayerMask);
            
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
            
#if UNITY_EDITOR
            _gizmosPoint = mouseWorldPos;
#endif
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_gizmosPoint, 0.1f);
        }
        
    }
}