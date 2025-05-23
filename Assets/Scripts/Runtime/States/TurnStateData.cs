using System;
using Runtime.Enums;

namespace Runtime.States
{
    [Serializable]
    public class TurnStateData
    {
        private bool _playerDrewCard;
        private bool _aiDrewCard;
        private bool _playerPassed;
        private bool _aiPassed;

        private TurnState _currentTurnState;

        public void SetPlayerDrewCard(bool value) => _playerDrewCard = value;
        public void SetAIDrewCard(bool value) => _aiDrewCard = value;
        public void SetPlayerPassed(bool value) => _playerPassed = value;
        public void SetAIPassed(bool value) => _aiPassed = value;
        
        public void SetCurrentTurnState(TurnState state) => _currentTurnState = state;
        public TurnState GetCurrentTurnState() => _currentTurnState;

        public void Reset()
        {
            _playerDrewCard = false;
            _aiDrewCard = false;
            _playerPassed = false;
            _aiPassed = false;
            _currentTurnState = TurnState.PlayerTurn;
        }

        public bool ShouldEndRound()
        {
            return _playerPassed && _aiPassed && !_playerDrewCard && !_aiDrewCard;
        }
    }
} 