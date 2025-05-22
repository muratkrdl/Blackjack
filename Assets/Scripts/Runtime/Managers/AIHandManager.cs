using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.Abstracts.Classes;
using Runtime.Enums;
using Runtime.Events;
using Runtime.Keys;
using Runtime.Utilities;
using UnityEngine;

namespace Runtime.Managers
{
    public class AIHandManager : HandManager
    {
        [SerializeField] private float minThinkTime = 0.5f;
        [SerializeField] private float maxThinkTime = 2f;

        private CancellationTokenSource cts = new();
        
        protected override void SubscribeEvents()
        {
            base.SubscribeEvents();
            CoreGameEvents.Instance.OnPlayerPlayCard += OnPlayerPlaySpecialCard;
        }

        protected override void UnSubscribeEvents()
        {
            base.UnSubscribeEvents();
            CoreGameEvents.Instance.OnPlayerPlayCard -= OnPlayerPlaySpecialCard;
        }

        private void OnPlayerPlaySpecialCard(PlayCardParams param)
        {
            
        }

        protected override void OnPass(HandManager hand)
        {
            base.OnPass(hand);
            if (hand != this)
            {
                ThinkAndPlayCard().Forget();
            }
            else
            {
                cts.Cancel();
                cts = new CancellationTokenSource();
            }
        }

        private async UniTaskVoid ThinkAndPlayCard()
        {
            float thinkTime = Random.Range(minThinkTime, maxThinkTime);
            
            await UnitaskUtilities.WaitForSecondsAsync(thinkTime);
            
            CardObject cardToPlay = DecideSpecialCardToPlay();
            
            if(cardToPlay)
            {
                PlaySpecialCard(cardToPlay);
            }
            else
            {
                int target = GameSettingsManager.Instance.GetCurrentTargetScore();
                if (Random.Range(_currentScore, target) < 18)
                {
                    CoreGameEvents.Instance.OnDrawCardFromBoard?.Invoke(new DrawCardParams()
                    {
                        HandManager = null,
                        Type = DrawCardTypes.Normal
                    });
                    ThinkAndPlayCard().Forget();
                }
                else
                {
                    Debug.Log("AI Pass");
                    CoreGameEvents.Instance.OnPass?.Invoke(this);
                }
            }
        }

        private CardObject DecideSpecialCardToPlay()
        {
            // TODO: Implement AI decision ^^
            
            // if (_handSpecialCards.Count > 0)
            // {
            //     return _handSpecialCards[0];
            // }
            
            // Draw or Pass
            return null;
        }

        protected override void UpdateScoreDisplay()
        {
            int boardScore = GameSettingsManager.Instance.GetCurrentTargetScore();
            int showScore = _currentScore - _handNormalCards.First().GetCardValue();
            
            scoreText.text = $"?+{showScore.ToString()}/{boardScore}";
        }
    }
} 