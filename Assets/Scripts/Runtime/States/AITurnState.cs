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

        public void OnPass(BaseHandManager baseHand)
        {
            if (baseHand is PlayerHandManager) return;

            _stateData.SetAIPassed(true);

            if (_stateData.ShouldEndRound())
            {
                EndRound();
            }
            else
            {
                CoreGameEvents.Instance.OnRoundEnd?.Invoke();
                CoreGameEvents.Instance.OnTurnChanged?.Invoke(TurnState.AITurn);
            }
        }

        public void OnDrawCard(BaseHandManager baseHand)
        {
            if (baseHand is AIHandManager)
            {
                _stateData.SetAIDrewCard(true);
            }
        }

        private void EndRound()
        {
            Debug.Log("End Round");
            _stateData.SetCurrentTurnState(TurnState.EndGame);
            CoreGameEvents.Instance.OnTurnChanged?.Invoke(TurnState.EndGame);
            CoreGameEvents.Instance.OnRoundEnd?.Invoke();
        }
    }
} 