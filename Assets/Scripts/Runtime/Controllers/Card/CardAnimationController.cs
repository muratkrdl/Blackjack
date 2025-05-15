using DG.Tweening;
using Runtime.Data.ValueObject;
using Runtime.Utilities;
using UnityEngine;

namespace Runtime.Controllers.Card
{
    public class CardAnimationController : MonoBehaviour
    {
        private CardAnimationData _data;
        private Transform _cardTransform;

        public void SetData(CardAnimationData data)
        {
            _data = data;
            _cardTransform = transform.GetChild(0).transform;
        }
        
        public void OnPointerEnter()
        {
            MovePos(_data.AnimPos);
        }

        public void OnPointerExit()
        {
            MovePos(ConstantsUtilities.Zero2);
        }

        private void MovePos(Vector3 pos)
        {
            _cardTransform.DOMove(pos, _data.Duration).SetEase(_data.EaseMode);
        }
        
    }
}