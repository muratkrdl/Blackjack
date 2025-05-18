using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Runtime.Abstracts.Classes;
using Runtime.Enums;
using Runtime.Events;
using Runtime.Keys;
using Runtime.Objects;
using Runtime.Utilities;
using TMPro;
using UnityEngine;

namespace Runtime.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private string playerName;
        
        [SerializeField] private Transform[] normalCardPoses;
        [SerializeField] private Transform[] specialCardPoses;

        [SerializeField] private TextMeshPro scoreText;
        
        private readonly Stack<CardObject> _handNormalCards = new();
        private readonly Stack<CardObject> _handSpecialCards = new();
        
        private int _currentScore;

        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            CoreGameEvents.Instance.OnDrawCard += OnDrawCrad;
            
            
            // TemporaryEvents
            InputEvents.Instance.OnNormalCardDraw += OnNormalCardDraw;
            InputEvents.Instance.OnSpecialCardDraw += OnSpecialCardDraw;
        }
        
        
        private void OnDrawCrad(DrawCardParams drawCardParams)
        {
            if (drawCardParams.PlayerManager != this) return;

            bool isNormal = drawCardParams.Obj.GetCurrentCardType() == CardTypes.Normal;
            PlayerSetDrawedCardParams param = new PlayerSetDrawedCardParams()
            {
                Cards = isNormal ? _handNormalCards : _handSpecialCards,
                Poses = isNormal ? normalCardPoses : specialCardPoses,
                DrawedCard = drawCardParams.Obj
            };
            
            DrawCard(param);
        }

        private void OnNormalCardDraw()
        {
            BoardManager.Instance.DrawCardToPlayer(this, DrawCardTypes.Normal);
        }

        private void OnSpecialCardDraw()
        {
            BoardManager.Instance.DrawCardToPlayer(this, DrawCardTypes.Special);
        }

        private void UnSubscribeEvents()
        {
            CoreGameEvents.Instance.OnDrawCard -= OnDrawCrad;
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
            param.Cards.Push(param.DrawedCard);
            param.DrawedCard.DrawCard(this);
            param.DrawedCard.MoveCard(param.Poses[param.Cards.Count-1]);
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
        
    }
}