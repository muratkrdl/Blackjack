using DG.Tweening;
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
        
        private Transform _cardVisualTransform;

        public void SetData(CardAnimationData data)
        {
            _moveData = data.AnimationMoveData;
            _scaleData = data.AnimationScaleData;
            _rotateData = data.AnimationRotateData;
            _cardVisualTransform = transform.GetChild(0).transform;
        }
        
        public void OnCardSpawn()
        {
            ScaleVisualCard();
            RotateVisualCard();
        }
        
        public void OnPointerEnter()
        {
            MoveVisualCard(_moveData.AnimPos);
        }

        public void OnPointerExit()
        {
            MoveVisualCard(ConstantsUtilities.Zero2);
        }

        private void ScaleVisualCard()
        {
            _cardVisualTransform.localScale = ConstantsUtilities.Zero2;
            _cardVisualTransform.DOScale(1f, _scaleData.Duration).SetEase(_scaleData.EaseMode);
        }

        private void RotateVisualCard()
        {
            _cardVisualTransform.DORotate(ConstantsUtilities.Zero3, _rotateData.Duration).SetEase(_rotateData.EaseMode);
        }

        private void MoveVisualCard(Vector3 pos)
        {
            _cardVisualTransform.DOLocalMove(pos, _moveData.Duration).SetEase(_moveData.EaseMode);
        }
        
    }
}