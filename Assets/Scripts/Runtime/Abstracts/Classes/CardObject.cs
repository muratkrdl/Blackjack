using Runtime.Controllers.Card;
using Runtime.Data.UnityObject;
using Runtime.Enums;
using Runtime.Managers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.Abstracts.Classes
{
    public abstract class CardObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] protected SpriteRenderer spriteRenderer;

        protected Card CardSoData;

        private CardMoveController _cardMoveController;
        private CardAnimationController _cardAnimationController;

        protected void Awake()
        {
            GetReferences();
            SetDatas();
        }

        private void GetReferences()
        {
            _cardMoveController = GetComponent<CardMoveController>();
            _cardAnimationController = GetComponent<CardAnimationController>();
        }

        private void SetDatas()
        {
            CD_CARD data = Resources.Load<CD_CARD>("Data/Cards/CD_CARD");
            _cardMoveController.SetData(data.MoveData);
            _cardAnimationController.SetData(data.AnimationData);
        }

        public virtual void SetCardSoData(Card cardData)
        {
            _cardAnimationController.SetCardData(cardData);
            _cardAnimationController.OnCardSpawn();
            CardSoData = cardData;
            spriteRenderer.sprite = CardSoData.CardImage;
        }
        
        public void MoveCard(Transform tra)
        {
            _cardMoveController.GoPos(tra.position);
            transform.SetParent(tra);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _cardAnimationController.OnPointerEnter();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _cardAnimationController.OnPointerExit();
        }

        public abstract void ReleasePool();

        public int GetCardValue() => CardSoData.CardValue;
        public CardTypes GetCurrentCardType() => CardSoData.Type;
        public void DrawCard(HandManager handManager) => CardSoData.DrawCard(handManager);
        public void DiscardCard(HandManager handManager) => CardSoData.DiscardCard(handManager);
        public void PlayCard(HandManager handManager) => (CardSoData as SpecialCard)?.PlayCard(handManager);
        
    }
}
