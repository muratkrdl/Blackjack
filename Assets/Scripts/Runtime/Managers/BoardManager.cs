using System.Collections.Generic;
using Runtime.Abstracts.Classes;
using Runtime.Data.UnityObject.Cards;
using Runtime.Events;
using Runtime.Extensions;
using Runtime.Keys;
using Runtime.Objects;
using Runtime.Systems.ObjectPool;
using UnityEngine;

namespace Runtime.Managers
{
    public class BoardManager : Monosingleton<BoardManager>
    {
        [SerializeField] private Transform normalCardSpawnTransform;
        [SerializeField] private Transform specialCardSpawnTransform;
        
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

        // TODO : Single Method For Every Card (if you can Murat ^^)
        public void DrawNormalwCardToPlayer(PlayerManager playerManager)
        {
            CoreGameEvents.Instance.OnDrawCard?.Invoke(new DrawCardParams()
            {
                PlayerManager = playerManager,
                Obj = GetNormalCard(),
            });
        }
        public void DrawSpecialCardToPlayer(PlayerManager playerManager)
        {
            CoreGameEvents.Instance.OnDrawCard?.Invoke(new DrawCardParams()
            {
                PlayerManager = playerManager,
                Obj = GetSpecialCard(),
            });
        }

        // TODO : Single Method For Every Card
        private CardObject GetNormalCard()
        {
            var cardObject = CardObjPool.Instance.Get();
            var card = _useNormalCards[Random.Range(0, _useNormalCards.Count)];
            cardObject.transform.position = normalCardSpawnTransform.position;
            cardObject.SetCardSOData(card);
            _useNormalCards.Remove(card);
            return cardObject;
        }

        private CardObject GetSpecialCard()
        {
            var cardObject = CardObjPool.Instance.Get();
            var card = _useSpecialCards[Random.Range(0, _useSpecialCards.Count)];
            cardObject.transform.position = specialCardSpawnTransform.position;
            cardObject.SetCardSOData(card);
            _useSpecialCards.Remove(card);
            return cardObject;
        }
        
    }
}