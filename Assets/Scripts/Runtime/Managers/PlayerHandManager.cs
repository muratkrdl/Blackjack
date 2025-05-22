using Runtime.Abstracts.Classes;
using Runtime.Events;
using Runtime.Keys;
using Runtime.Strategy.HandScore;
using UnityEngine;

namespace Runtime.Managers
{
    public class PlayerHandManager : HandManager
    {
        protected override void Awake()
        {
            base.Awake();
            _handScoreStrategy = GetComponent<PlayerHandScoreStrategy>();
        }

        protected override void SubscribeEvents()
        {
            base.SubscribeEvents();
            CoreGameEvents.Instance.OnPlayerPlayCard += OnPlayerPlayCard;
        }

        protected override void UnSubscribeEvents()
        {
            base.UnSubscribeEvents();
            CoreGameEvents.Instance.OnPlayerPlayCard -= OnPlayerPlayCard;
        }

        protected override void OnPass(HandManager hand)
        {
            if (hand != this)
            {
                // Can Play
                
            }
            else
            {
                // Can not Play
                
            }
        }

        private void OnPlayerPlayCard(PlayCardParams param)
        {
            
        }

    }
} 