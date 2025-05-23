using DG.Tweening;
using Runtime.Abstracts.Classes;
using Runtime.Data.ValueObject.CardAnim;
using Runtime.Utilities;
using UnityEngine;

namespace Runtime.Strategy.CardAnimation
{
    public class NormalCardAnimationStrategy : BaseCardAnimationStrategy
    {
        public NormalCardAnimationStrategy(Transform cardVisualTransform, CardAnimationData data, Animator animator)
            : base(cardVisualTransform, data, animator)
        {
        }

        public override void OnCardSpawn()
        {
            _cardVisualTransform.localScale = ConstantsUtilities.Zero2;
            
            Sequence sequence = DOTween.Sequence();
            
            sequence
                .Append(ScaleVisualCard(_scaleData.BaseTarget))
                .Join(RotateVisualCard(_rotationData.BaseTarget));
        }

        public override void OnCardDespawn()
        {
            _animator.SetTrigger(ConstantsUtilities.NormalDestroy);
        }

        public override void OnPointerEnter()
        {
            Sequence sequence = DOTween.Sequence();
            
            sequence
                .Append(ScaleVisualCard(_scaleData.NormalTarget))
                .Join(MoveVisualCard(_moveData.AnimPos));
        }

        public override void OnPointerExit()
        {
            Sequence sequence = DOTween.Sequence();
            
            sequence
                .Append(ScaleVisualCard(_scaleData.BaseTarget))
                .Join(MoveVisualCard(ConstantsUtilities.Zero2));
        }
    }
}