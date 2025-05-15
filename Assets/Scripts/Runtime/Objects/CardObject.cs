using Runtime.Abstracts.Classes;
using Runtime.Abstracts.Interfaces;
using Runtime.Controllers.Card;
using Runtime.Data.UnityObject;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Pool;

namespace Runtime.Objects
{
    public class CardObject : MonoBehaviour, IPoolableObj<CardObject>, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        
        private Card _cardData;
        private CD_CARD _data;
        
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

        public void SetCardData(Card cardData)
        {
            _cardData = cardData;
        }
        
        public void SetPool(ObjectPool<CardObject> pool)
        {
            _pool = pool;
        }

        public void MoveCard(Vector3 pos)
        {
            _cardMoveController.GoPos(pos);
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