using System.Collections.Generic;
using Runtime.Abstracts.Classes;
using Runtime.Abstracts.Interfaces;
using Runtime.Enums;
using Runtime.Events;
using Runtime.Keys;
using TMPro;
using UnityEngine;

namespace Runtime.Managers
{
    public abstract class HandManager : MonoBehaviour, IHandManager
    {
        [SerializeField] protected string playerName;
        
        [SerializeField] protected Transform[] normalCardPoses;
        [SerializeField] protected Transform[] specialCardPoses;

        [SerializeField] protected TextMeshPro scoreText;

        protected List<CardObject> _handNormalCards = new();
        protected List<CardObject> _handSpecialCards = new();
        
        protected int _currentScore;
        protected int _cardsInHand;

        protected bool _canPlay;
        protected bool _passed;

        protected virtual void OnEnable()
        {
            SubscribeEvents();
        }
        
        protected virtual void SubscribeEvents()
        {
            CoreGameEvents.Instance.OnDrawedCardToHand += OnDrawedCardToHand;
            CoreGameEvents.Instance.OnPass += OnPass;
            CoreGameEvents.Instance.OnReset += OnReset;
        }

        protected virtual void UnSubscribeEvents()
        {
            CoreGameEvents.Instance.OnDrawedCardToHand -= OnDrawedCardToHand;
            CoreGameEvents.Instance.OnPass -= OnPass;
            CoreGameEvents.Instance.OnReset -= OnReset;
        }

        protected virtual void OnDisable()
        {
            UnSubscribeEvents();
        }

        protected virtual void OnDrawedCardToHand(DrawedCardParams drawCardParams)
        {
            if (drawCardParams.HandManager != this) return;

            bool isNormal = drawCardParams.Obj.GetCurrentCardType() == CardTypes.Normal;
            PlayerSetDrawedCardParams param = new PlayerSetDrawedCardParams()
            {
                Cards = isNormal ? _handNormalCards : _handSpecialCards,
                Poses = isNormal ? normalCardPoses : specialCardPoses,
                DrawedVisualCard = drawCardParams.Obj
            };
            
            DrawCard(param);
        }
        
        protected virtual void OnPass(HandManager hand)
        {
            if (hand == this)
            {
                _passed = true;
                _canPlay = false;
            }
            else
            {
                _passed = false;
                _canPlay = true;
            }
        }
        
        protected virtual void DrawCard(PlayerSetDrawedCardParams param)
        {
            _cardsInHand++;
            param.Cards.Add(param.DrawedVisualCard);
            param.DrawedVisualCard.DrawCard(this);
            param.DrawedVisualCard.MoveCard(param.Poses[param.Cards.Count-1]);
        }

        protected virtual void OnReset()
        {
            ResetHand();
        }

        protected virtual void ResetHand()
        {
            ResetCardList(_handNormalCards);
            ResetCardList(_handSpecialCards);
            
            _currentScore = 0;
            _cardsInHand = 0;
            UpdateScoreDisplay();
        }

        protected virtual void ResetCardList(List<CardObject> cardList)
        {
            foreach (var card in cardList)
            {
                card.ReleasePool();
            }
            cardList.Clear();
        }
        
        public virtual void PlaySpecialCard(CardObject card)
        {
            card.PlayCard(this);
            _handSpecialCards.Remove(card);
        }

        public virtual void IncreaseScore(int value)
        {
            _currentScore += value;
            UpdateScoreDisplay();
        }

        public virtual void DecreaseScore(int value)
        {
            _currentScore -= value;
            UpdateScoreDisplay();
        }

        protected virtual void UpdateScoreDisplay()
        { 
            int boardScore = GameSettingsManager.Instance.GetCurrentTargetScore();
            
            var colorCode = _currentScore switch
            {
                _ when _currentScore > boardScore => "#FF8D8D", // Red
                _ when _currentScore == boardScore => "#8DFF8D", // Green
                _ => "#FFFFFF" // White
            };
            scoreText.text = $"<color={colorCode}>{_currentScore.ToString()}</color>/{boardScore}";
        }

        public virtual int GetCurrentScore() => _currentScore;
        public virtual int GetCardsInHand() => _cardsInHand;
        public virtual int GetNormalCardInHand() => _handNormalCards.Count;
        public virtual int GetSpecialCardInHand() => _handSpecialCards.Count;
        public virtual string GetPlayerName() => playerName;
    }
}