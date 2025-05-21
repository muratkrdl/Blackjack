using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Runtime.Abstracts.Classes;
using Runtime.Data.UnityObject.Cards;
using Runtime.Enums;
using Runtime.Events;
using Runtime.Extensions;
using Runtime.Keys;
using Runtime.Systems.ObjectPool;
using Runtime.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Runtime.Managers
{
    public class BoardManager : Monosingleton<BoardManager>
    {
        [Header("Hand References")]
        [SerializeField] private HandManager playerHand;
        [SerializeField] private HandManager enemyHand;
        
        [Header("Spawn Points")]
        [SerializeField] private Transform normalCardSpawnPoint;
        [SerializeField] private Transform specialCardSpawnPoint;

        private List<NormalCard> _initialNormalCards;
        private List<SpecialCard> _initialSpecialCards;
        private List<NormalCard> _useNormalCards;
        private List<SpecialCard> _useSpecialCards;

        protected override void Awake()
        {
            base.Awake();
            SetData();
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            CoreGameEvents.Instance.OnReset += OnReset;
            CoreGameEvents.Instance.OnTourStart += OnTourStart;
        }

        private void UnSubscribeEvents()
        {
            CoreGameEvents.Instance.OnReset -= OnReset;
            CoreGameEvents.Instance.OnTourStart -= OnTourStart;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void OnReset()
        {
            ResetCardPools();
        }

        private void OnTourStart()
        {
            StartTour().Forget();
        }

        private void SetData()
        {
            _initialNormalCards = new List<NormalCard>(Resources.LoadAll<NormalCard>("Data/Cards/Normal"));
            _initialSpecialCards = new List<SpecialCard>(Resources.LoadAll<SpecialCard>("Data/Cards/Special"));
            ResetCardPools();
        }

        private void ResetCardPools()
        {
            _useNormalCards = new List<NormalCard>(_initialNormalCards);
            _useSpecialCards = new List<SpecialCard>(_initialSpecialCards);
        }

        private void DrawCardToHand(HandManager handManager, DrawCardTypes cardType)
        {
            List<Card> selectedList = GetCardListByType(cardType);
            Transform spawnPoint = GetSpawnPointByType(cardType);
            CardObject cardObject = CreateCard(selectedList, spawnPoint, handManager.GetCardsInHand() == 0);

            CoreGameEvents.Instance.OnDrawCard?.Invoke(new DrawCardParams
            {
                HandManager = handManager,
                Obj = cardObject
            });
        }

        private List<Card> GetCardListByType(DrawCardTypes cardType)
        {
            return cardType == DrawCardTypes.Special ? _useSpecialCards.Cast<Card>().ToList() : _useNormalCards.Cast<Card>().ToList();
        }

        private Transform GetSpawnPointByType(DrawCardTypes cardType)
        {
            return cardType == DrawCardTypes.Special ? specialCardSpawnPoint : normalCardSpawnPoint;
        }

        private CardObject CreateCard<T>(List<T> cardList, Transform spawnTransform, bool isFirst) where T : Card
        {
            CardObject cardObj = isFirst ? FirstCardObjPool.Instance.Get() : VisualCardObjPool.Instance.Get();
            T card = cardList[Random.Range(0, cardList.Count)];
            cardObj.transform.position = spawnTransform.position;
            cardObj.SetCardSoData(card);
            cardList.Remove(card);
            return cardObj;
        }

        // TODO : Delete These Func
        private async UniTaskVoid StartTour()
        {
            await DealInitialCards();
        }
        private async UniTask DealInitialCards()
        {
            await DealCardPair(DrawCardTypes.Normal);
            await DealCardPair(DrawCardTypes.Special);
            await DealCardPair(DrawCardTypes.Normal);
            await DealCardPair(DrawCardTypes.Special);
        }
        private async UniTask DealCardPair(DrawCardTypes cardType)
        {
            PlayerCard(cardType);
            await UnitaskUtilities.WaitForSecondsAsync(.25f);
            EnemyCard(cardType);
            await UnitaskUtilities.WaitForSecondsAsync(.25f);
        }
        
        public void PlayerCard(DrawCardTypes type)
        {
            DrawCardToHand(playerHand, type);
        }

        public void EnemyCard(DrawCardTypes type)
        {
            DrawCardToHand(enemyHand, type);
        }
        
    }
}