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

        private BaseHandManager _baseHandOwner;
        private bool _isInteractable = true;
        
        private void Awake()
        {
            _baseHandOwner = GetComponent<BaseHandManager>();
            
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
            if (_baseHandOwner.GetNormalCardInHand() >= GameSettingsManager.Instance.GetMaxNormalCard()) return;
            
            CoreGameEvents.Instance.OnDrawCardFromBoard?.Invoke(new DrawCardParams()
            {
                BaseHandManager = _baseHandOwner,
                Type = DrawCardTypes.Normal
            });
        }
        
        private void OnClick_PassCardButton()
        {
            if (!_isInteractable) return;
            CoreGameEvents.Instance.OnPass?.Invoke(_baseHandOwner);
        }
    }
}
