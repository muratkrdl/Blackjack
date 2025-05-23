using System.Collections.Generic;
using System.Linq;
using Runtime.Abstracts.Classes;
using Runtime.Enums;
using Runtime.Keys;
using UnityEngine;

namespace Runtime.Controllers.Hand
{
    public class HandCardController : MonoBehaviour
    {
        [SerializeField] private Transform[] normalCardPoses;
        [SerializeField] private Transform[] specialCardPoses;

        private List<CardObject> _handNormalCards = new();
        private List<CardObject> _handSpecialCards = new();
        
        private int _cardsInHand;

        private BaseHandManager _owner;

        private void Awake()
        {
            _owner = GetComponent<BaseHandManager>();
        }

        public void OnDrewCardToHand(DrawedCardParams drawCardParams)
        {
            if (drawCardParams.BaseHandManager != _owner) return;
            
            bool isNormal = drawCardParams.Obj.GetCurrentCardType() == CardTypes.Normal;
            PlayerSetDrawedCardParams param = new PlayerSetDrawedCardParams()
            {
                Cards = isNormal ? _handNormalCards : _handSpecialCards,
                Poses = isNormal ? normalCardPoses : specialCardPoses,
                DrawedVisualCard = drawCardParams.Obj
            };
            
            DrawCard(param);
        }
        
        private void DrawCard(PlayerSetDrawedCardParams param)
        {
            _cardsInHand++;
            param.Cards.Add(param.DrawedVisualCard);
            param.DrawedVisualCard.DrawCard(_owner);
            param.DrawedVisualCard.MoveCard(param.Poses[param.Cards.Count-1]);
        }

        public void Reset()
        {
            ResetCardList(_handNormalCards);
            ResetCardList(_handSpecialCards);
            _cardsInHand = 0;
        }
        
        private void ResetCardList(List<CardObject> cardList)
        {
            foreach (var card in cardList)
            {
                card.ReleasePool();
            }
            cardList.Clear();
        }
        
        public void PlaySpecialCard(CardObject card)
        {
            card.PlayCard(_owner);
            _handSpecialCards.Remove(card);
        }
        
        public CardObject GetFirstNormalCard() => _handNormalCards.FirstOrDefault();
        public int GetCardsInHand() => _cardsInHand;
        public int GetNormalCardInHand() => _handNormalCards.Count;
        public int GetSpecialCardInHand() => _handSpecialCards.Count;
        
    }
}