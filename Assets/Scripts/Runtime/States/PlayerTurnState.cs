using Runtime.Abstracts.Classes;
using Runtime.Enums;
using Runtime.Events;
using Runtime.Managers;
using UnityEngine;

namespace Runtime.States
{
    public class PlayerTurnState : ITurnState
    {
        private readonly TurnStateData _stateData;

        public PlayerTurnState(TurnStateData stateData)
        {
            _stateData = stateData;
            _stateData.SetCurrentTurnState(TurnState.PlayerTurn);
        }

        public void OnPass(HandManager hand)
        {
            if (hand is not PlayerHandManager) return;

            _stateData.SetPlayerPassed(true);

            TurnManager.Instance.ChangeState(new AITurnState(_stateData));
        }

        public void OnDrawCard(HandManager hand)
        {
            if (hand is PlayerHandManager)
            {
                _stateData.SetPlayerDrewCard(true);
            }
        }

        private void EndRound()
        {
            Debug.Log("Ending Round");
            _stateData.SetCurrentTurnState(TurnState.EndGame);
            CoreGameEvents.Instance.OnTurnChanged?.Invoke(_stateData.CurrentTurnState);
            CoreGameEvents.Instance.OnRoundEnd?.Invoke();
        }
    }
} 