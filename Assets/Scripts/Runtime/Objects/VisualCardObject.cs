using Runtime.Abstracts.Classes;
using Runtime.Abstracts.Interfaces;
using Runtime.Data.UnityObject.Cards;
using Runtime.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

namespace Runtime.Objects
{
    public class VisualCardObject : CardObject, IPoolableObj<VisualCardObject>
    {
        [SerializeField] private TextMeshPro scoreText;
        
        private ObjectPool<VisualCardObject> _pool;

        public override void SetCardSoData(Card cardData, IHandManager owner)
        {
            base.SetCardSoData(cardData, owner);
            scoreText.text = cardData.CardValue.ToString();

            if (owner.GetFirstNormalCard()) // Has Card
            {
                spriteRenderer.sprite = cardData is NormalCard ? CardSoData.CardImage : CardSoData.CardBackImage;
                return;
            }
            
            spriteRenderer.sprite = CardSoData.CardBackImage;
            switch (owner)
            {
                case PlayerHandManager:
                    ShowNumber();
                    break;
                case AIHandManager:
                    HideNumber();
                    break;
            }
        }
        
        public void HideNumber()
        {
            scoreText.gameObject.SetActive(false);
        }

        public void ShowNumber()
        {
            scoreText.gameObject.SetActive(true);
        }

        public void SetPool(ObjectPool<VisualCardObject> pool)
        {
            _pool = pool;
        }

        public override void ReleasePool()
        {
            _pool.Release(this);
        }
    }
}