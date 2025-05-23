using Runtime.Abstracts.Interfaces;
using Runtime.Controllers.Hand;
using Runtime.Events;
using Runtime.Keys;
using UnityEngine;

namespace Runtime.Abstracts.Classes
{
    public abstract class BaseHandManager : MonoBehaviour, IHandManager
    {
        [SerializeField] protected string playerName;
        
        protected HandCardController _handCardController;
        protected BaseHandScoreStrategy BaseHandScoreStrategy;
        
        protected virtual void Awake()
        {
            _handCardController = GetComponent<HandCardController>();
        }

        protected virtual void OnEnable()
        {
            SubscribeEvents();
        }
        
        protected virtual void SubscribeEvents()
        {
            CoreGameEvents.Instance.OnDrewCardToHand += OnDrewCardToHand;
            CoreGameEvents.Instance.OnTourEnd += OnTourEnd;
            CoreGameEvents.Instance.OnReset += OnReset;
        }

        protected virtual void UnSubscribeEvents()
        {
            CoreGameEvents.Instance.OnDrewCardToHand -= OnDrewCardToHand;
            CoreGameEvents.Instance.OnTourEnd -= OnTourEnd;
            CoreGameEvents.Instance.OnReset -= OnReset;
        }

        protected virtual void OnDisable()
        {
            UnSubscribeEvents();
        }

        protected virtual void OnDrewCardToHand(DrawedCardParams drawCardParams) => _handCardController.OnDrewCardToHand(drawCardParams);

        private void OnTourEnd()
        {
            
        }

        protected virtual void OnReset()
        {
            _handCardController.Reset();
            BaseHandScoreStrategy.Reset();
        }

        public virtual void PlaySpecialCard(CardObject card)
        {
            _handCardController.PlaySpecialCard(card);
        }

        public virtual void IncreaseScore(int value) => BaseHandScoreStrategy.IncreaseScore(value);
        public virtual void DecreaseScore(int value) => BaseHandScoreStrategy.DecreaseScore(value);

        public virtual CardObject GetFirstNormalCard() => _handCardController.GetFirstNormalCard();
        public virtual int GetCurrentScore() => BaseHandScoreStrategy.GetCurrentScore();
        public virtual int GetCardsInHand() => _handCardController.GetCardsInHand();
        public virtual int GetNormalCardInHand() => _handCardController.GetNormalCardInHand();
        public virtual int GetSpecialCardInHand() => _handCardController.GetSpecialCardInHand();
        public virtual string GetPlayerName() => playerName;
    }
}