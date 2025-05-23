using Runtime.Abstracts.Classes;
using Runtime.Enums;
using Runtime.Events;
using Runtime.Extensions;
using Runtime.Keys;
using Runtime.States;
using UnityEngine;

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
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameEvents.Instance.OnPass += OnPass;
            CoreGameEvents.Instance.OnRoundStart += OnRoundStart;
            CoreGameEvents.Instance.OnRoundEnd += OnRoundEnd;
            CoreGameEvents.Instance.OnDrawCardFromBoard += OnDrawCardFromBoard;
            CoreGameEvents.Instance.OnTurnChanged += OnTurnChanged;
        }
        private void UnSubscribeEvents()
        {
            CoreGameEvents.Instance.OnPass -= OnPass;
            CoreGameEvents.Instance.OnRoundStart -= OnRoundStart;
            CoreGameEvents.Instance.OnRoundEnd -= OnRoundEnd;
            CoreGameEvents.Instance.OnDrawCardFromBoard -= OnDrawCardFromBoard;
            CoreGameEvents.Instance.OnTurnChanged -= OnTurnChanged;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void Start()
        {
            _stateData.Reset();
        }
        
        private void OnPass(BaseHandManager baseHand)
        {
            _currentState.OnPass(baseHand);
            TurnState newState = baseHand is PlayerHandManager ? TurnState.AITurn : TurnState.PlayerTurn;
            CoreGameEvents.Instance.OnTurnChanged?.Invoke(newState);
        }
        
        private void OnRoundStart()
        {
            _stateData.Reset();
            CoreGameEvents.Instance.OnTurnChanged(TurnState.PlayerTurn);
        }
        
        private void OnRoundEnd()
        {
            CoreGameEvents.Instance.OnRoundStart?.Invoke();
        }

        private void OnDrawCardFromBoard(DrawCardParams param)
        {
            _currentState.OnDrawCard(param.BaseHandManager);
        }
        
        private void OnTurnChanged(TurnState newState)
        {
            ITurnState newTurnState = newState switch
            {
                TurnState.PlayerTurn => new PlayerTurnState(_stateData),
                TurnState.AITurn => new AITurnState(_stateData),
                _ => null
            };
            
            _currentState = newTurnState;
        }

        public TurnState GetCurrentTurn() => _stateData.GetCurrentTurnState();
    }
}