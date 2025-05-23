using Cysharp.Threading.Tasks;
using Runtime.Controllers.Board;
using Runtime.Enums;
using Runtime.Events;
using Runtime.Extensions;
using Runtime.Keys;
using Runtime.Utilities;
using UnityEngine;

namespace Runtime.Managers
{
    public class BoardManager : Monosingleton<BoardManager>
    {
        [SerializeField] private PlayerHandManager playerHand;
        [SerializeField] private AIHandManager aiHand;
        
        private DrawCardController _drawCardController;
        
        protected override void Awake()
        {
            base.Awake();
            SetDatas();
        }
        
        private void SetDatas()
        {
            _drawCardController = GetComponent<DrawCardController>();
            _drawCardController.SetData();
            
            _drawCardController.Reset();
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            CoreGameEvents.Instance.OnReset += OnReset;
            CoreGameEvents.Instance.OnDrawCardFromBoard += OnDrawCardFromBoard;
            CoreGameEvents.Instance.OnGameStart += OnGameStart;
        }

        private void UnSubscribeEvents()
        {
            CoreGameEvents.Instance.OnReset -= OnReset;
            CoreGameEvents.Instance.OnDrawCardFromBoard -= OnDrawCardFromBoard;
            CoreGameEvents.Instance.OnGameStart -= OnGameStart;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void OnReset()
        {
            _drawCardController.Reset();
        }
        
        private void OnDrawCardFromBoard(DrawCardParams param)
        {
            _drawCardController.OnDrawCardFromBoard(param);
        }
        
        private void OnGameStart()
        {
            CoreGameEvents.Instance.OnRoundStart?.Invoke();
            GameStart();
        }

        private void GameStart()
        {
            DealInitialCards().Forget();
        }

        // Delete These Funcs
        private async UniTask DealInitialCards()
        {
            await DealCardOnTourStart(DrawCardTypes.Normal);
            await DealCardOnTourStart(DrawCardTypes.Special);
            await DealCardOnTourStart(DrawCardTypes.Normal);
            
            CoreGameEvents.Instance.OnRoundStart?.Invoke();
        }

        private async UniTask DealCardOnTourStart(DrawCardTypes cardType)
        {
            PlayerCard(cardType);
            await UnitaskUtilities.WaitForSecondsAsync(.25f);
            EnemyCard(cardType);
            await UnitaskUtilities.WaitForSecondsAsync(.25f);
        }
        
        private void PlayerCard(DrawCardTypes type)
        {
            CoreGameEvents.Instance.OnDrawCardFromBoard?.Invoke(new DrawCardParams()
            {
                BaseHandManager = playerHand,
                Type = type
            });
        }

        private void EnemyCard(DrawCardTypes type)
        {
            CoreGameEvents.Instance.OnDrawCardFromBoard?.Invoke(new DrawCardParams()
            {
                BaseHandManager = aiHand,
                Type = type
            });
        }
    }
}