using System.Collections.Generic;
using Runtime.Abstracts.Classes;
using Runtime.Data.UnityObject.Cards;
using Runtime.Extensions;
using Runtime.Objects;
using Runtime.Systems.ObjectPool;
using UnityEngine;

namespace Runtime.Managers
{
    public class BoardManager : Monosingleton<BoardManager>
    {
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
        
        public CardObject GetNormalCard()
        {
            var cardObject = CardObjPool.Instance.Get();
            var card = _useNormalCards[Random.Range(0, _useNormalCards.Count)];
            cardObject.SetCardSOData(card);
            _useNormalCards.Remove(card);
            return cardObject;
        }
        
        public CardObject GetSpecialCard()
        {
            var cardObject = CardObjPool.Instance.Get();
            var card = _useSpecialCards[Random.Range(0, _useSpecialCards.Count)];
            cardObject.SetCardSOData(card);
            _useSpecialCards.Remove(card);
            return cardObject;
        }
        
    }
}