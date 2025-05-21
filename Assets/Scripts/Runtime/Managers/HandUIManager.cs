using Runtime.Enums;
using Runtime.Events;
using Runtime.Keys;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Managers
{
    public class HandUIManager : MonoBehaviour
    {
        [SerializeField] private Button DrawCardButton;
        [SerializeField] private Button PassButton;

        private HandManager _handOwner;
        
        private void Awake()
        {
            _handOwner = GetComponent<HandManager>();
            
            DrawCardButton.onClick.AddListener(OnClick_DrawCardButton);
            PassButton.onClick.AddListener(OnClick_PassCardButton);
        }

        private void OnClick_DrawCardButton()
        {
            CoreGameEvents.Instance.OnDrawCardFromBoard?.Invoke(new DrawCardParams()
            {
                HandManager = _handOwner,
                Type = DrawCardTypes.Normal
            });
        }
        
        private void OnClick_PassCardButton()
        {
            CoreGameEvents.Instance.OnDrawCardFromBoard?.Invoke(new DrawCardParams()
            {
                HandManager = null,
                Type = DrawCardTypes.Normal
            });
        }
    }
}
