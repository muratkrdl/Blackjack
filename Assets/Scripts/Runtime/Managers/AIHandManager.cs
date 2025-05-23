using System.Threading;
using Cysharp.Threading.Tasks;
using Runtime.Abstracts.Classes;
using Runtime.Enums;
using Runtime.Events;
using Runtime.Keys;
using Runtime.Strategy.HandScore;
using Runtime.Utilities;
using UnityEngine;

namespace Runtime.Managers
{
    public class AIHandManager : BaseHandManager
    {
        [SerializeField] private float minThinkTime = 0.5f;
        [SerializeField] private float maxThinkTime = 2f;

        private CancellationTokenSource _cts = new();

        protected override void Awake()
        {
            base.Awake();
            BaseHandScoreStrategy = GetComponent<AIHandScoreStrategy>();
        }

        protected override void SubscribeEvents()
        {
            base.SubscribeEvents();
            CoreGameEvents.Instance.OnPlayerPlayCard += OnPlayerPlaySpecialCard;
            CoreGameEvents.Instance.OnTurnChanged += OnTurnChanged;
        }

        protected override void UnSubscribeEvents()
        {
            base.UnSubscribeEvents();
            CoreGameEvents.Instance.OnPlayerPlayCard -= OnPlayerPlaySpecialCard;
            CoreGameEvents.Instance.OnTurnChanged -= OnTurnChanged;
        }
        
        private void OnTurnChanged(TurnState newState)
        {
            if (newState == TurnState.AITurn)
            {
                ThinkAndPlayCard().Forget();
            }
            else
            {
                _cts.Cancel();
                _cts = new CancellationTokenSource();
            }
        }

        private void OnPlayerPlaySpecialCard(PlayCardParams param)
        {
            
        }

        private async UniTaskVoid ThinkAndPlayCard()
        {
            float thinkTime = Random.Range(minThinkTime, maxThinkTime);
            
            await UnitaskUtilities.WaitForSecondsAsync(thinkTime, _cts);
            
            CardObject cardToPlay = DecideSpecialCardToPlay();
            
            if(cardToPlay)
            {
                PlaySpecialCard(cardToPlay);
            }
            else
            {
                int target = GameSettingsManager.Instance.GetCurrentTargetScore();
                if (Random.Range(GetCurrentScore(), target) < 18)
                {
                    CoreGameEvents.Instance.OnDrawCardFromBoard?.Invoke(new DrawCardParams()
                    {
                        BaseHandManager = this,
                        Type = DrawCardTypes.Normal
                    });
                    ThinkAndPlayCard().Forget();
                }
                else
                {
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
        
    }
} 