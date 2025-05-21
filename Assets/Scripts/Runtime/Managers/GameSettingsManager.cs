using Runtime.Extensions;
using UnityEngine;

namespace Runtime.Managers
{
    public class GameSettingsManager : Monosingleton<GameSettingsManager>
    {
        private CD_GAME_SETTINGS _data;

        private int _currentTargetScore;

        protected override void Awake()
        {
            base.Awake();
            _data = Resources.Load<CD_GAME_SETTINGS>("Data/CD_GAME_SETTINGS");
            _currentTargetScore = _data.gameSettings.InitialTargetScore;
        }

        public int GetCurrentTargetScore()
        {
            return _currentTargetScore;
        }
        
    }
}