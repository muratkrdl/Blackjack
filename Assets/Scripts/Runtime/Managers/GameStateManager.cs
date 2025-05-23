using Runtime.Events;
using Runtime.Extensions;

namespace Runtime.Managers
{
    public class GameStateManager : Monosingleton<GameStateManager>
    {
        // GameStates _currentState;
        
        private void Start()
        {
            CoreGameEvents.Instance.OnGameStart?.Invoke();
        }

        private void OnEnable()
        {
            
        }
        
    }
}