using Runtime.Abstracts.Classes;
using Runtime.Enums;
using Runtime.Events;
using Runtime.Extensions;
using Runtime.Keys;
using Runtime.States;

namespace Runtime.Managers
{
    public class TurnManager : Monosingleton<TurnManager>
    {
        private ITurnState _currentState;
        private TurnStateData _stateData;

        protected override void Awake()
        {
            _stateData = new TurnStateData();
            _currentState = new PlayerTurnState(_stateData);
        }

        private void OnEnable()
        {
            CoreGameEvents.Instance.OnPass += OnPass;
            CoreGameEvents.Instance.OnRoundStart += OnRoundStart;
            CoreGameEvents.Instance.OnRoundEnd += OnRoundEnd;
            CoreGameEvents.Instance.OnDrawCardFromBoard += OnDrawCardFromBoard;
        }

        private void OnDisable()
        {
            CoreGameEvents.Instance.OnPass -= OnPass;
            CoreGameEvents.Instance.OnRoundStart -= OnRoundStart;
            CoreGameEvents.Instance.OnRoundEnd -= OnRoundEnd;
            CoreGameEvents.Instance.OnDrawCardFromBoard -= OnDrawCardFromBoard;
        }

        private void Start()
        {
            _stateData.Reset();
            _currentState = new PlayerTurnState(_stateData);
            CoreGameEvents.Instance.OnGameStart?.Invoke();
        }
        
        private void OnRoundStart()
        {
            _stateData.Reset();
        }
        
        private void OnRoundEnd()
        {
            CoreGameEvents.Instance.OnRoundStart?.Invoke();
        }

        private void OnDrawCardFromBoard(DrawCardParams param)
        {
            _currentState.OnDrawCard(param.HandManager);
        }

        private void OnPass(HandManager hand)
        {
            _currentState.OnPass(hand);
        }

        public void ChangeState(ITurnState newState)
        {
            _currentState = newState;
            CoreGameEvents.Instance.OnTurnChanged?.Invoke(_stateData.CurrentTurnState);
        }

        public TurnState GetCurrentTurn() => _stateData.CurrentTurnState;
    }
}