using System.Collections.Generic;
using Runtime.Abstracts.Classes;
using UnityEngine;

namespace Runtime.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private string playerName;
        
        [SerializeField] private Transform[] normalCardPoses;
        [SerializeField] private Transform[] specialCardPoses;
        
        private readonly Stack<Card> _handNormalCards = new();
        private readonly Stack<Card> _handSpecialCards = new();
        
        private int _currentScore;

        public void IncreaseScore(int value)
        {
            _currentScore += value;
            // UIManager.Instance.UpdateScore(this, currentScore);
        }
        public void DecreaseScore(int value)
        {
            _currentScore += value;
            // UIManager.Instance.UpdateScore(this, currentScore);
        }

        public void DrawCard(Card card)
        {
            _handNormalCards.Push(card);
            // UIManager.Instance.ShowCard(card, this);
        }

        public void PlayCard(Card card)
        {
            card.PlayCard(this);
            _handNormalCards.Pop();
        }

        public int GetCurrentScore()
        {
            return _currentScore;
        }
        
    }
}