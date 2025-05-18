using Runtime.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Button DrawCardButton;
        [SerializeField] private Button PassButton;

        private void Awake()
        {
            DrawCardButton.onClick.AddListener(OnClick_DrawCardButton);
            PassButton.onClick.AddListener(OnClick_PassCardButton);
        }

        private void OnClick_DrawCardButton()
        {
            BoardManager.Instance.PlayerCard(DrawCardTypes.Normal);
        }
        
        private void OnClick_PassCardButton()
        {
            BoardManager.Instance.EnemyCard(DrawCardTypes.Normal);
        }
    }
}
