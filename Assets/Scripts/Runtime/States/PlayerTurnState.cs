using Runtime.Abstracts.Classes;
using Runtime.Enums;
using Runtime.Events;
using Runtime.Managers;

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

        public void OnPass(BaseHandManager baseHand)
        {
            if (baseHand is not PlayerHandManager) return;

            _stateData.SetPlayerPassed(true);

            CoreGameEvents.Instance.OnTurnChanged?.Invoke(TurnState.PlayerTurn);
        }

        public void OnDrawCard(BaseHandManager baseHand)
        {
            if (baseHand is PlayerHandManager)
            {
                _stateData.SetPlayerDrewCard(true);
            }
        }

        // private void EndRound()
        // {
        //     Debug.Log("Ending Round");
        //     _stateData.SetCurrentTurnState(TurnState.EndGame);
        //     CoreGameEvents.Instance.OnTurnChanged?.Invoke(TurnState.EndGame);
        //     CoreGameEvents.Instance.OnRoundEnd?.Invoke();
        // }
        
    }
} 