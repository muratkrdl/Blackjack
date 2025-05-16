using Runtime.Abstracts.Classes;
using Runtime.Abstracts.Interfaces;
using Runtime.Controllers.Card;
using Runtime.Data.UnityObject;
using Runtime.Data.UnityObject.Cards;
using Runtime.Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Pool;

namespace Runtime.Objects
{
    public class CardObject : MonoBehaviour, IPoolableObj<CardObject>, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        
        private CD_CARD _data;
        private Card _cardSOData;
        
        private CardMoveController _cardMoveController;
        private CardAnimationController _cardAnimationController;
        
        private ObjectPool<CardObject> _pool;

        private void Awake()
        {
            GetReferences();
            SetDatas();
        }
        
        private void GetReferences()
        {
            _cardMoveController = GetComponent<CardMoveController>();
            _cardAnimationController = GetComponent<CardAnimationController>();
        }
        
        private void SetDatas()
        {
            _data = Resources.Load<CD_CARD>("Data/Cards/CD_CARD");
            _cardMoveController.SetData(_data.MoveData);
            _cardAnimationController.SetData(_data.AnimationData);
        }

        public void SetCardSOData(Card cardData)
        {
            _cardAnimationController.OnCardSpawn();
            _cardSOData = cardData;
            spriteRenderer.sprite = _cardSOData.CardImage;
        }
        
        public void SetPool(ObjectPool<CardObject> pool)
        {
            _pool = pool;
        }
        public void ReleasePool()
        {
            _pool.Release(this);
        }

        public int GetCardValue()
        {
            var obj = (_cardSOData as NormalCard);
            return obj != null ? obj.CardValue : 0;
        }

        public void DrawCard(PlayerManager playerManager)
        {
            
        }

        public void DiscardCard(PlayerManager playerManager)
        {
            
        }
        
        public void PlayCard(PlayerManager playerManager)
        {
            
        }

        public void MoveCard(Transform tra)
        {
            _cardMoveController.GoPos(tra.position);
            transform.SetParent(tra);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _cardAnimationController.OnPointerEnter();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _cardAnimationController.OnPointerExit();
        }
        
    }
}