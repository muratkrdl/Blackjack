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
        protected readonly CardAnimationRotationData _rotationData;

        protected BaseCardAnimationStrategy(Transform cardVisualTransform, CardAnimationData data)
        {
            _cardVisualTransform = cardVisualTransform;
            _moveData = data.AnimationMoveData;
            _scaleData = data.AnimationScaleData;
            _rotationData = data.animationRotationData;
        }

        public virtual void OnCardSpawn()
        {
            _cardVisualTransform.localScale = ConstantsUtilities.Zero2;
            ScaleVisualCard(_scaleData.BaseTarget);
            RotateVisualCard(_rotationData.BaseTarget);
        }

        public virtual void OnPointerEnter()
        {
            MoveVisualCard(_moveData.AnimPos);
        }

        public virtual void OnPointerExit()
        {
            MoveVisualCard(ConstantsUtilities.Zero2);
        }

        protected Tween RotateVisualCard(Vector3 target)
        {
            return _cardVisualTransform.DORotate(target, _rotationData.Duration).SetEase(_rotationData.EaseMode);
        }
        
        protected Tween ScaleVisualCard(float target)
        {
            return _cardVisualTransform.DOScale(target, _scaleData.Duration).SetEase(_scaleData.EaseMode);
        }

        protected Tween MoveVisualCard(Vector3 pos)
        {
            return _cardVisualTransform.DOLocalMove(pos, _moveData.Duration).SetEase(_moveData.EaseMode);
        }
        
    }
}