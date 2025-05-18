using DG.Tweening;
using Runtime.Data.ValueObject;
using Runtime.Data.ValueObject.CardAnim;
using Runtime.Utilities;
using UnityEngine;

namespace Runtime.Controllers.Card
{
    public class CardAnimationController : MonoBehaviour
    {
        private CardAnimationMoveData _moveData;
        private CardAnimationScaleData _scaleData;
        private CardAnimationRotateData _rotateData;
        
        private Transform _cardTransform;

        public void SetData(CardAnimationData data)
        {
            _moveData = data.AnimationMoveData;
            _scaleData = data.AnimationScaleData;
            _rotateData = data.AnimationRotateData;
            _cardTransform = transform.GetChild(0).transform;
        }
        
        public void OnPointerEnter()
        {
            MoveVisualCard(_moveData.AnimPos);
        }

        public void OnPointerExit()
        {
            MoveVisualCard(ConstantsUtilities.Zero2);
        }

        public void OnCardSpawn()
        {
            ScaleVisualCard();
            RotateVisualCard();
        }

        private void ScaleVisualCard()
        {
            _cardTransform.localScale = ConstantsUtilities.Zero2;
            _cardTransform.DOScale(1f, _scaleData.Duration / 2).SetEase(_scaleData.EaseMode);
        }

        private void RotateVisualCard()
        {
            _cardTransform.DORotate(ConstantsUtilities.Zero3, _rotateData.Duration).SetEase(_rotateData.EaseMode);
        }

        private void MoveVisualCard(Vector3 pos)
        {
            _cardTransform.DOLocalMove(pos, _moveData.Duration).SetEase(_moveData.EaseMode);
        }
        
    }
}