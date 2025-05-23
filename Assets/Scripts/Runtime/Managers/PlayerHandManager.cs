using Runtime.Abstracts.Classes;
using Runtime.Enums;
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
            CoreGameEvents.Instance.OnTurnChanged += OnTurnChanged;
        }

        protected override void UnSubscribeEvents()
        {
            base.UnSubscribeEvents();
            CoreGameEvents.Instance.OnPlayerPlayCard -= OnPlayerPlayCard;
            CoreGameEvents.Instance.OnTurnChanged -= OnTurnChanged;
        }

        private void OnTurnChanged(TurnState newState)
        {
            bool isPlayerTurn = newState == TurnState.PlayerTurn;
            SetInteractable(isPlayerTurn);
        }

        private void OnPlayerPlayCard(PlayCardParams param)
        {
            if (TurnManager.Instance.GetCurrentTurn() != TurnState.PlayerTurn) return;
            
            // Handle card play
        }

        private void SetInteractable(bool interactable)
        {
            var handUI = GetComponent<HandUIManager>();
            if (handUI)
            {
                handUI.SetInteractable(interactable);
            }
        }
    }
} 