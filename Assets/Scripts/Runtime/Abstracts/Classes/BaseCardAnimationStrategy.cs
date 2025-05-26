using DG.Tweening;
using Runtime.Abstracts.Interfaces;
using Runtime.Data.ValueObject.CardAnim;
using Runtime.Strategy.CardAnimation;
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

        protected readonly CardObject _cardObject;
        protected readonly Animator _animator;
        
        protected BaseCardAnimationStrategy(Transform cardVisualTransform, CardAnimationData data, Animator animator, CardObject cardObject)
        {
            _cardVisualTransform = cardVisualTransform;
            _cardObject = cardObject;
            _animator = animator;
            _moveData = data.AnimationMoveData;
            _scaleData = data.AnimationScaleData;
            _rotationData = data.animationRotationData;
        }

        public abstract void OnCardSpawn();
        public abstract void OnCardDespawn();
        public virtual void OnDiscard()
        {
            if (_cardObject.IsBackCardImage())
            {
                Sequence sequence = DOTween.Sequence();
            
                sequence
                    .Append(ScaleVisualCard(_scaleData.NormalTarget))
                    .Join(MoveVisualCard(_moveData.AnimPos))
                    .Join(RotateVisualCard(_rotationData.SpecialTargetFirst).OnComplete(() => _cardObject.SetBackCardImage()))
                    .Append(RotateVisualCard(_rotationData.SpecialTargetSecond).OnComplete(SetTriggerAnimation));
            }
            else
            {
                SetTriggerAnimation();
            }
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
        
        private void SetTriggerAnimation()
        {
            bool isNormal = this is NormalCardAnimationStrategy;
            _animator.SetTrigger(isNormal ? ConstantsUtilities.NormalDestroy : ConstantsUtilities.SpecialDestroy);
        }
        
    }
}