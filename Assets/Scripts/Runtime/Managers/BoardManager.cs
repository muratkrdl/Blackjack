using System.Collections.Generic;
using Runtime.Abstracts.Classes;
using Runtime.Data.UnityObject.Cards;
using Runtime.Enums;
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

        public void DrawCardToPlayer(PlayerManager playerManager, DrawCardTypes cardType) // NormalCard or SpecialCard
        {
            CardObject returnObj = cardType == DrawCardTypes.Normal
                ? GetCard(_useNormalCards, normalCardSpawnPoint)
                : GetCard(_useSpecialCards, specialCardSpawnPoint);
            
            CoreGameEvents.Instance.OnDrawCard?.Invoke(new DrawCardParams()
            {
                PlayerManager = playerManager,
                Obj = returnObj
            });
        }
        
        private CardObject GetCard<T>(List<T> cardList, Transform spawnTransform) where T : Card
        {
            CardObject cardObj = CardObjPool.Instance.Get();
            T card = cardList[Random.Range(0, cardList.Count)];
            cardObj.transform.position = spawnTransform.position;
            cardObj.SetCardSOData(card);
            cardList.Remove(card);
            return cardObj;
        }
        
    }
}