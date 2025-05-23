using Runtime.Abstracts.Classes;
using Runtime.Enums;
using Runtime.Events;
using Runtime.Keys;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Managers
{
    public class HandUIManager : MonoBehaviour
    {
        [SerializeField] private Button drawCardButton;
        [SerializeField] private Button passButton;

        private HandManager _handOwner;
        private bool _isInteractable = true;
        
        private void Awake()
        {
            _handOwner = GetComponent<HandManager>();
            
            drawCardButton.onClick.AddListener(OnClick_DrawCardButton);
            passButton.onClick.AddListener(OnClick_PassCardButton);
        }

        public void SetInteractable(bool interactable)
        {
            _isInteractable = interactable;
            drawCardButton.interactable = interactable;
            passButton.interactable = interactable;
        }

        private void OnClick_DrawCardButton()
        {
            if (!_isInteractable) return;
            if (_handOwner.GetNormalCardInHand() >= GameSettingsManager.Instance.GetMaxNormalCard()) return;
            
            CoreGameEvents.Instance.OnDrawCardFromBoard?.Invoke(new DrawCardParams()
            {
                HandManager = _handOwner,
                Type = DrawCardTypes.Normal
            });
        }
        
        private void OnClick_PassCardButton()
        {
            if (!_isInteractable) return;
            CoreGameEvents.Instance.OnPass?.Invoke(_handOwner);
        }
    }
}
