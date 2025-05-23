using System.Collections.Generic;
using System.Linq;
using Runtime.Abstracts.Classes;
using Runtime.Data.UnityObject.Cards;
using Runtime.Enums;
using Runtime.Events;
using Runtime.Keys;
using Runtime.Managers;
using Runtime.Systems.ObjectPool;
using Runtime.Utilities;
using UnityEngine;
using MyCard = Runtime.Abstracts.Classes.Card;

namespace Runtime.Controllers.Board
{
    public class DrawCardController : MonoBehaviour
    {
        [SerializeField] private Transform normalCardSpawnPoint;
        [SerializeField] private Transform specialCardSpawnPoint;
        
        private List<NormalCard> _initialNormalCards;
        private List<SpecialCard> _initialSpecialCards;
        
        private List<NormalCard> _useNormalCards;
        private List<SpecialCard> _useSpecialCards;

        public void SetData()
        {
            _initialNormalCards = new List<NormalCard>(Resources.LoadAll<NormalCard>("Data/Cards/Normal"));
            _initialSpecialCards = new List<SpecialCard>(Resources.LoadAll<SpecialCard>("Data/Cards/Special"));
            Reset();
        }

        public void Reset()
        {
            _useNormalCards = new List<NormalCard>(_initialNormalCards);
            _useSpecialCards = new List<SpecialCard>(_initialSpecialCards);
        }
        
        public void OnDrawCardFromBoard(DrawCardParams param)
        {
            BaseHandManager baseHand = param.BaseHandManager;

            int newLayer = (baseHand is PlayerHandManager) ? ConstantsUtilities.InteractableLayer: ConstantsUtilities.UnInteractableLayer;
            
            List<MyCard> selectedList = GetCardListByType(param.Type);
            Transform spawnPoint = GetSpawnPointByType(param.Type);
            CardObject cardObject = CreateCard(selectedList, spawnPoint, baseHand);
            
            cardObject.gameObject.layer = newLayer;

            CoreGameEvents.Instance.OnDrewCardToHand?.Invoke(new DrawedCardParams
            {
                BaseHandManager = baseHand,
                Obj = cardObject
            });
        }
        
        private List<MyCard> GetCardListByType(DrawCardTypes cardType)
        {
            return cardType == DrawCardTypes.Special ? _useSpecialCards.Cast<MyCard>().ToList() : _useNormalCards.Cast<MyCard>().ToList();
        }

        private Transform GetSpawnPointByType(DrawCardTypes cardType)
        {
            return cardType == DrawCardTypes.Special ? specialCardSpawnPoint : normalCardSpawnPoint;
        }

        private CardObject CreateCard<T>(List<T> cardList, Transform spawnTransform, BaseHandManager baseHand) where T : MyCard
        {
            CardObject cardObj = VisualCardObjPool.Instance.Get();
            T card = cardList[Random.Range(0, cardList.Count)];
            cardObj.transform.position = spawnTransform.position;
            cardObj.SetCardSoData(card, baseHand);
            RemoveFromList(card);
            return cardObj;
        }
        
        private void RemoveFromList<T>(T card) where T : MyCard
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
        
    }
}