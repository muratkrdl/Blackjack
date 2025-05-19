using DG.Tweening;
using Runtime.Abstracts.Interfaces;
using Runtime.Data.ValueObject.CardAnim;
using Runtime.Utilities;
using UnityEngine;

namespace Runtime.Abstracts.Classes
{
    public abstract class BaseCardAnimationStrategy : ICardAnimationStrategy
    {
        protected readonly Transform _cardVisualTransform;
        protected readonly CardAnimationMoveData _moveData;
        protected readonly CardAnimationScaleData _scaleData;
        protected readonly CardAnimationRotateData _rotateData;

        protected BaseCardAnimationStrategy(Transform cardVisualTransform, CardAnimationData data)
        {
            _cardVisualTransform = cardVisualTransform;
            _moveData = data.AnimationMoveData;
            _scaleData = data.AnimationScaleData;
            _rotateData = data.AnimationRotateData;
        }

        public virtual void OnCardSpawn()
        {
            ScaleVisualCard();
            RotateVisualCard();
        }

        public virtual void OnPointerEnter()
        {
            MoveVisualCard(_moveData.AnimPos);
        }

        public virtual void OnPointerExit()
        {
            MoveVisualCard(ConstantsUtilities.Zero2);
        }

        private void ScaleVisualCard()
        {
            _cardVisualTransform.localScale = ConstantsUtilities.Zero2;
            _cardVisualTransform.DOScale(_scaleData.Target, _scaleData.Duration).SetEase(_scaleData.EaseMode);
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