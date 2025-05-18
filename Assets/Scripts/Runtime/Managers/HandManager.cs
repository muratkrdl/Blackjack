using System.Collections.Generic;
using Runtime.Abstracts.Classes;
using Runtime.Enums;
using Runtime.Events;
using Runtime.Interfaces;
using Runtime.Keys;
using TMPro;
using UnityEngine;

namespace Runtime.Managers
{
    public class HandManager : MonoBehaviour, IHandManager
    {
        [SerializeField] private string playerName;
        
        [SerializeField] private Transform[] normalCardPoses;
        [SerializeField] private Transform[] specialCardPoses;

        [SerializeField] private TextMeshPro scoreText;
        
        private Stack<CardObject> _handNormalCards = new();
        private Stack<CardObject> _handSpecialCards = new();

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

        private void OnReset()
        {
            foreach (var card in _handNormalCards)
            {
                card.ReleasePool();
            }
            _handNormalCards = new Stack<CardObject>();
            
            foreach (var card in _handSpecialCards)
            {
                card.ReleasePool();
            }
            _handSpecialCards = new Stack<CardObject>();
            
            _currentScore = 0;
            _cardsInHand = 0;
            SetScoreText(_currentScore);
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

        public void IncreaseScore(int value)
        {
            _currentScore += value;
            SetScoreText(_currentScore);
        }
        public void DecreaseScore(int value)
        {
            _currentScore -= value;
            SetScoreText(_currentScore);
        }

        private void DrawCard(PlayerSetDrawedCardParams param)
        {
            _cardsInHand++;
            param.Cards.Push(param.DrawedVisualCard);
            param.DrawedVisualCard.DrawCard(this);
            param.DrawedVisualCard.MoveCard(param.Poses[param.Cards.Count-1]);
        }
        
        public void PlaySpecialCard(SpecialCard card)
        {
            card.PlayCard(this);
            _handSpecialCards.Pop();
        }
        
        private void SetScoreText(int score)
        { 
            // TODO : GetBoardScore For Text Instead of "21"
            int boardScore = 21;
            
            var colorCode = score switch
            {
                _ when score > boardScore => "#FF8D8D", // Red
                _ when score == boardScore => "#8DFF8D", // Green
                _ => "#FFFFFF" // White
            };
            scoreText.text = $"<color={colorCode}>{score.ToString()}</color>/{boardScore}";
        }

        public int GetCurrentScore()
        {
            return _currentScore;
        }

        public int GetCardsInHand()
        {
            return _cardsInHand;
        }

        public void DrawCard(DrawCardTypes type)
        {
            // TODO : Implement Murat ^^
            
        }
    }
}