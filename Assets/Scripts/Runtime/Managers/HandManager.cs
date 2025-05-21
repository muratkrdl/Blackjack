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
    public class HandManager : MonoBehaviour, IHandManager
    {
        [Header("Player Info")]
        [SerializeField] private string playerName;
        
        [Header("Card Positions")]
        [SerializeField] private Transform[] normalCardPoses;
        [SerializeField] private Transform[] specialCardPoses;

        [Header("UI Elements")]
        [SerializeField] private TextMeshPro scoreText;

        private List<CardObject> _handNormalCards = new();
        private List<CardObject> _handSpecialCards = new();
        
        private int _currentScore;
        private int _cardsInHand;

        private void OnEnable()
        {
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            CoreGameEvents.Instance.OnDrawCard += OnDrawCard;
            CoreGameEvents.Instance.OnReset += OnReset;
        }

        private void UnSubscribeEvents()
        {
            CoreGameEvents.Instance.OnDrawCard -= OnDrawCard;
            CoreGameEvents.Instance.OnReset -= OnReset;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void OnDrawCard(DrawCardParams drawCardParams)
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
        
        private void DrawCard(PlayerSetDrawedCardParams param)
        {
            _cardsInHand++;
            param.Cards.Add(param.DrawedVisualCard);
            param.DrawedVisualCard.DrawCard(this);
            param.DrawedVisualCard.MoveCard(param.Poses[param.Cards.Count-1]);
        }

        private void OnReset()
        {
            ResetHand();
        }

        private void ResetHand()
        {
            ResetCardList(_handNormalCards);
            ResetCardList(_handSpecialCards);
            
            _currentScore = 0;
            _cardsInHand = 0;
            UpdateScoreDisplay();
        }

        private void ResetCardList(List<CardObject> cardList)
        {
            foreach (var card in cardList)
            {
                card.ReleasePool();
            }
            cardList.Clear();
        }
        
        public void PlaySpecialCard(CardObject card)
        {
            card.PlayCard(this);
            _handSpecialCards.Remove(card);
        }

        public void IncreaseScore(int value)
        {
            _currentScore += value;
            UpdateScoreDisplay();
        }

        public void DecreaseScore(int value)
        {
            _currentScore -= value;
            UpdateScoreDisplay();
        }

        private void UpdateScoreDisplay()
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

        public int GetCurrentScore() => _currentScore;
        public int GetCardsInHand() => _cardsInHand;
        public string GetPlayerName() => playerName;
    }
}