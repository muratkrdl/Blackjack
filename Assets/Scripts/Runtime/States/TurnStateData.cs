using System;
using Runtime.Abstracts.Classes;
using Runtime.Enums;

namespace Runtime.States
{
    [Serializable]
    public class TurnStateData
    {
        public bool PlayerDrewCard { get; private set; }
        public bool AIDrewCard { get; private set; }
        public bool PlayerPassed { get; private set; }
        public bool AIPassed { get; private set; }
        public TurnState CurrentTurnState { get; private set; }

        public void SetPlayerDrewCard(bool value) => PlayerDrewCard = value;
        public void SetAIDrewCard(bool value) => AIDrewCard = value;
        public void SetPlayerPassed(bool value) => PlayerPassed = value;
        public void SetAIPassed(bool value) => AIPassed = value;
        
        public void SetCurrentTurnState(TurnState state) => CurrentTurnState = state;

        public void Reset()
        {
            PlayerDrewCard = false;
            AIDrewCard = false;
            PlayerPassed = false;
            AIPassed = false;
            CurrentTurnState = TurnState.PlayerTurn;
        }

        public bool ShouldEndRound()
        {
            return PlayerPassed && AIPassed && !PlayerDrewCard && !AIDrewCard;
        }
    }
} 