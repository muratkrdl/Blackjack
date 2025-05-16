using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Runtime.Abstracts.Classes;
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

        private void Start()
        {
            GetCardAsync().Forget();
        }

        private async UniTaskVoid GetCardAsync()
        {
            await UnitaskUtilities.WaitForSecondsAsync(.5f);
            DrawNormalCard(BoardManager.Instance.GetNormalCard());
            await UnitaskUtilities.WaitForSecondsAsync(.25f);
            DrawSpecialCard(BoardManager.Instance.GetSpecialCard());
            await UnitaskUtilities.WaitForSecondsAsync(.5f);
            DrawNormalCard(BoardManager.Instance.GetNormalCard());
            await UnitaskUtilities.WaitForSecondsAsync(.25f);
            DrawSpecialCard(BoardManager.Instance.GetSpecialCard());
            await UnitaskUtilities.WaitForSecondsAsync(.5f);
            DrawNormalCard(BoardManager.Instance.GetNormalCard());
            await UnitaskUtilities.WaitForSecondsAsync(.25f);
            DrawSpecialCard(BoardManager.Instance.GetSpecialCard());
        }
        
        private void SetScoreText(int score)
        {
            scoreText.text = $"{score.ToString()}/21"; // TODO : GetBoardScoreForText
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

        public void DrawNormalCard(CardObject card)
        {
            _handNormalCards.Push(card);
            card.PlayCard(this);
            card.MoveCard(normalCardPoses[_handNormalCards.Count-1]);
        }
        
        public void DrawSpecialCard(CardObject card)
        {
            _handSpecialCards.Push(card);
            card.MoveCard(specialCardPoses[_handSpecialCards.Count-1]);
        }

        public void PlaySpecialCard(SpecialCard card)
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