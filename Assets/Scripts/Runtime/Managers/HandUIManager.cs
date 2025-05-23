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
        
        private void Awake()
        {
            _handOwner = GetComponent<HandManager>();
            
            drawCardButton.onClick.AddListener(OnClick_DrawCardButton);
            passButton.onClick.AddListener(OnClick_PassCardButton);
        }

        private void OnClick_DrawCardButton()
        {
            if (_handOwner.GetNormalCardInHand() >= GameSettingsManager.Instance.GetMaxNormalCard()) return; // TODO : Get From GameSetting
            
            CoreGameEvents.Instance.OnDrawCardFromBoard?.Invoke(new DrawCardParams()
            {
                HandManager = _handOwner,
                Type = DrawCardTypes.Normal
            });
        }
        
        private void OnClick_PassCardButton()
        {
            CoreGameEvents.Instance.OnPass?.Invoke(_handOwner);
        }
    }
}
