using Runtime.Abstracts.Interfaces;
using Runtime.Controllers.Card;
using Runtime.Data.UnityObject;
using Runtime.Enums;
using Runtime.Managers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.Abstracts.Classes
{
    public abstract class CardObject : MonoBehaviour //, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] protected SpriteRenderer spriteRenderer;

        protected Card CardSoData;

        private CardMoveController _cardMoveController;
        private CardAnimationController _cardAnimationController;

        protected IHandManager Owner;

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

        public virtual void SetCardSoData(Card cardData, IHandManager owner)
        {
            Owner = owner;
            _cardAnimationController.SetCardData(cardData, this);
            _cardAnimationController.OnCardSpawn();
            CardSoData = cardData;
        }
        
        public void MoveCard(Transform tra)
        {
            _cardMoveController.GoPos(tra.position);
            transform.SetParent(tra);
        }

        public void OnPointerEnter()
        {
            _cardAnimationController.OnPointerEnter();
        }

        public void OnPointerExit()
        {
            _cardAnimationController.OnPointerExit();
        }

        public abstract void ReleasePool();

        public void SetBackCardImage() => spriteRenderer.sprite = CardSoData.CardBackImage;
        public void SetNormalCardImage() => spriteRenderer.sprite = CardSoData.CardImage;
        public int GetCardValue() => CardSoData.CardValue;
        public CardTypes GetCurrentCardType() => CardSoData.Type;
        public void DrawCard(BaseHandManager baseHandManager) => CardSoData.DrawCard(baseHandManager);
        public void DiscardCard(BaseHandManager baseHandManager) => CardSoData.DiscardCard(baseHandManager);
        public void PlayCard(BaseHandManager baseHandManager) => (CardSoData as SpecialCard)?.PlayCard(baseHandManager);
        
    }
}
