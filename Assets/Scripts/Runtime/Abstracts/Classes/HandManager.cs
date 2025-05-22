using Runtime.Abstracts.Interfaces;
using Runtime.Controllers;
using Runtime.Events;
using Runtime.Keys;
using UnityEngine;

namespace Runtime.Abstracts.Classes
{
    public abstract class HandManager : MonoBehaviour, IHandManager
    {
        [SerializeField] protected string playerName;
        
        protected HandCardController _handCardController;
        protected HandScoreStrategy _handScoreStrategy;
        
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
            CoreGameEvents.Instance.OnDrawedCardToHand += OnDrawedCardToHand;
            CoreGameEvents.Instance.OnPass += OnPass;
            CoreGameEvents.Instance.OnTourEnd += OnTourEnd;
            CoreGameEvents.Instance.OnReset += OnReset;
        }

        protected virtual void UnSubscribeEvents()
        {
            CoreGameEvents.Instance.OnDrawedCardToHand -= OnDrawedCardToHand;
            CoreGameEvents.Instance.OnPass -= OnPass;
            CoreGameEvents.Instance.OnTourEnd -= OnTourEnd;
            CoreGameEvents.Instance.OnReset -= OnReset;
        }

        protected virtual void OnDisable()
        {
            UnSubscribeEvents();
        }

        protected virtual void OnDrawedCardToHand(DrawedCardParams drawCardParams) => _handCardController.OnDrawedCardToHand(drawCardParams);

        protected abstract void OnPass(HandManager hand);
        
        private void OnTourEnd()
        {
            
        }

        protected virtual void OnReset()
        {
            _handCardController.Reset();
            _handScoreStrategy.Reset();
        }

        public virtual void PlaySpecialCard(CardObject card)
        {
            _handCardController.PlaySpecialCard(card);
        }

        public virtual void IncreaseScore(int value) => _handScoreStrategy.IncreaseScore(value);
        public virtual void DecreaseScore(int value) => _handScoreStrategy.DecreaseScore(value);

        public virtual CardObject GetFirstNormalCard() => _handCardController.GetFirstNormalCard();
        public virtual int GetCurrentScore() => _handScoreStrategy.GetCurrentScore();
        public virtual int GetCardsInHand() => _handCardController.GetCardsInHand();
        public virtual int GetNormalCardInHand() => _handCardController.GetNormalCardInHand();
        public virtual int GetSpecialCardInHand() => _handCardController.GetSpecialCardInHand();
        public virtual string GetPlayerName() => playerName;
    }
}