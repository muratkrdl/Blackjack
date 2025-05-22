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
        [SerializeField] private PlayerHandManager playerHand;
        [SerializeField] private AIHandManager enemyHand;
        
        [SerializeField] private Transform normalCardSpawnPoint;
        [SerializeField] private Transform specialCardSpawnPoint;

        private List<NormalCard> _initialNormalCards;
        private List<SpecialCard> _initialSpecialCards;
        
        private List<NormalCard> _useNormalCards;
        private List<SpecialCard> _useSpecialCards;

        protected override void Awake()
        {
            base.Awake();
            SetDatas();
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            CoreGameEvents.Instance.OnReset += OnReset;
            CoreGameEvents.Instance.OnDrawCardFromBoard += OnDrawCardFromBoard;
            CoreGameEvents.Instance.OnTourStart += OnTourStart;
        }

        private void UnSubscribeEvents()
        {
            CoreGameEvents.Instance.OnReset -= OnReset;
            CoreGameEvents.Instance.OnDrawCardFromBoard -= OnDrawCardFromBoard;
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
            StartTour();
        }
        
        private void SetDatas()
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
        
        private void OnDrawCardFromBoard(DrawCardParams param)
        {
			HandManager hand = param.HandManager ? playerHand : enemyHand;

            List<Card> selectedList = GetCardListByType(param.Type);
            Transform spawnPoint = GetSpawnPointByType(param.Type);
            CardObject cardObject = CreateCard(selectedList, spawnPoint, hand);

            CoreGameEvents.Instance.OnDrawedCardToHand?.Invoke(new DrawedCardParams
            {
                HandManager = hand,
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

        private CardObject CreateCard<T>(List<T> cardList, Transform spawnTransform, HandManager hand) where T : Card
        {
            CardObject cardObj = VisualCardObjPool.Instance.Get();
            T card = cardList[Random.Range(0, cardList.Count)];
            cardObj.transform.position = spawnTransform.position;
            cardObj.SetCardSoData(card, hand);
            RemoveFromList(card);
            return cardObj;
        }
        
        private void RemoveFromList<T>(T card) where T : Card
        {
            if (_useNormalCards.Contains(card as NormalCard))
            {
                _useNormalCards.Remove(card as NormalCard);
            }
            else if (_useSpecialCards.Contains(card as SpecialCard))
            {
                _useSpecialCards.Remove(card as SpecialCard);
            }
        }

        private void StartTour()
        {
            DealInitialCards().Forget();
        }

        // Delete These Funcs
        private async UniTask DealInitialCards()
        {
            await DealCardOnTourStart(DrawCardTypes.Normal);
            await DealCardOnTourStart(DrawCardTypes.Special);
            await DealCardOnTourStart(DrawCardTypes.Normal);
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
                HandManager = playerHand,
                Type = type
            });
        }

        private void EnemyCard(DrawCardTypes type)
        {
            CoreGameEvents.Instance.OnDrawCardFromBoard?.Invoke(new DrawCardParams()
            {
                HandManager = null,
                Type = type
            });
        }
    }
}