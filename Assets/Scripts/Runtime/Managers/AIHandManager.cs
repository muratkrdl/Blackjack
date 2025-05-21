using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Runtime.Abstracts.Classes;
using Runtime.Abstracts.Interfaces;
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

        private bool _isThinking;

        protected override void SubscribeEvents()
        {
            base.SubscribeEvents();
            CoreGameEvents.Instance.OnPlayerPlayCard += OnPlayerPlaySpecialCard;
            CoreGameEvents.Instance.OnTourEnd += OnTourEnd;
        }

        protected override void UnSubscribeEvents()
        {
            base.UnSubscribeEvents();
            CoreGameEvents.Instance.OnPlayerPlayCard -= OnPlayerPlaySpecialCard;
            CoreGameEvents.Instance.OnTourEnd = OnTourEnd;
        }

        private void OnPlayerPlaySpecialCard(PlayCardParams param)
        {
            
        }
        
        private void OnTourEnd()
        {
            // if != this
            
            ThinkAndPlayCard().Forget();
        }
        
        private async UniTaskVoid ThinkAndPlayCard()
        {
            _isThinking = true;
            
            float thinkTime = Random.Range(minThinkTime, maxThinkTime);
            
            await UnitaskUtilities.WaitForSecondsAsync(thinkTime);
            
            CardObject cardToPlay = DecideSpecialCardToPlay();
            
            if (cardToPlay)
            {
                PlaySpecialCard(cardToPlay);
            }
            
            _isThinking = false;
        }

        private CardObject DecideSpecialCardToPlay()
        {
            // TODO: Implement AI decision ^^
            
            if (_handSpecialCards.Count > 0)
            {
                return _handSpecialCards[0];
            }
            
            // Draw or Pass
            return null;
        }

    }
} 