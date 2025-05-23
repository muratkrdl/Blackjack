using Runtime.Abstracts.Classes;
using Runtime.Enums;
using Runtime.Events;
using Runtime.Managers;
using UnityEngine;

namespace Runtime.States
{
    public class AITurnState : ITurnState
    {
        private readonly TurnStateData _stateData;

        public AITurnState(TurnStateData stateData)
        {
            _stateData = stateData;
            _stateData.SetCurrentTurnState(TurnState.AITurn);
        }

        public void OnPass(HandManager hand)
        {
            if (hand is not AIHandManager) return;

            _stateData.SetAIPassed(true);

            if (_stateData.ShouldEndRound())
            {
                EndRound();
            }
            else
            {
                CoreGameEvents.Instance.OnRoundEnd?.Invoke();
                TurnManager.Instance.ChangeState(new PlayerTurnState(_stateData));
            }
        }

        public void OnDrawCard(HandManager hand)
        {
            if (hand is AIHandManager)
            {
                _stateData.SetAIDrewCard(true);
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