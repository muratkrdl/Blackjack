using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Runtime.Abstracts.Classes;
using Runtime.Data.UnityObject.Cards;
using Runtime.Enums;
using Runtime.Events;
using Runtime.Extensions;
using Runtime.Keys;
using Runtime.Objects;
using Runtime.Systems.ObjectPool;
using Runtime.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Runtime.Managers
{
    public class BoardManager : Monosingleton<BoardManager>
    {
        [SerializeField] private HandManager playerHand;
        [SerializeField] private HandManager enemyHand;
        
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
        
        private void SetData()
        {
            _initialNormalCards = new List<NormalCard>(Resources.LoadAll<NormalCard>("Data/Cards/Normal"));
            _initialSpecialCards = new List<SpecialCard>(Resources.LoadAll<SpecialCard>("Data/Cards/Special"));
            _useNormalCards = new List<NormalCard>(_initialNormalCards);
            _useSpecialCards = new List<SpecialCard>(_initialSpecialCards);
        }

        private void OnEnable()
        {
            CoreGameEvents.Instance.OnReset += OnReset;
            CoreGameEvents.Instance.OnTourStart += OnTourStart;
        }

        private void OnReset()
        {
            _useNormalCards = new List<NormalCard>(_initialNormalCards);
            _useSpecialCards = new List<SpecialCard>(_initialSpecialCards);
        }
        
        private void OnTourStart()
        {
            TourStarted().Forget();
        }
        
        // TODO : Delete these
        public void PlayerCard(DrawCardTypes type)
        {
            DrawCardToPlayer(playerHand, type);
        }
        public void EnemyCard(DrawCardTypes type)
        {
            DrawCardToPlayer(enemyHand, type);
        }

        private async UniTaskVoid TourStarted()
        {
            PlayerCard(DrawCardTypes.Normal);
            await UnitaskUtilities.WaitForSecondsAsync(.25f);
            EnemyCard(DrawCardTypes.Normal);
            await UnitaskUtilities.WaitForSecondsAsync(.25f);
            PlayerCard(DrawCardTypes.Special);
            await UnitaskUtilities.WaitForSecondsAsync(.25f);
            EnemyCard(DrawCardTypes.Special);
            await UnitaskUtilities.WaitForSecondsAsync(.25f);
            PlayerCard(DrawCardTypes.Normal);
            await UnitaskUtilities.WaitForSecondsAsync(.25f);
            EnemyCard(DrawCardTypes.Normal);
            await UnitaskUtilities.WaitForSecondsAsync(.25f);
            PlayerCard(DrawCardTypes.Special);
            await UnitaskUtilities.WaitForSecondsAsync(.25f);
            EnemyCard(DrawCardTypes.Special);
        }

        private void OnDisable()
        {
            CoreGameEvents.Instance.OnReset -= OnReset;
            CoreGameEvents.Instance.OnTourStart -= OnTourStart;
        }

        private void DrawCardToPlayer(HandManager handManager, DrawCardTypes cardType)
        {
            List<Card> selectedList = cardType == DrawCardTypes.Special ? _useSpecialCards.Cast<Card>().ToList() : _useNormalCards.Cast<Card>().ToList();
            Transform spawnPoint = cardType == DrawCardTypes.Special ? specialCardSpawnPoint : normalCardSpawnPoint;

            CardObject returnObj = GetCard(selectedList, spawnPoint, handManager.GetCardsInHand() == 0);

            CoreGameEvents.Instance.OnDrawCard?.Invoke(new DrawCardParams()
            {
                HandManager = handManager,
                Obj = returnObj
            });
        }
        
        private CardObject GetCard<T>(List<T> cardList, Transform spawnTransform, bool isFirst) where T : Card
        {
            CardObject cardObj = isFirst ? FirstCardObjPool.Instance.Get() : VisualCardObjPool.Instance.Get();
            T card = cardList[Random.Range(0, cardList.Count)];
            cardObj.transform.position = spawnTransform.position;
            cardObj.SetCardSoData(card);
            cardList.Remove(card);
            return cardObj;
        }
        
    }
}