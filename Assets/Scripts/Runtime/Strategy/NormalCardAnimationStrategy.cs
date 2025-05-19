using DG.Tweening;
using Runtime.Abstracts.Classes;
using Runtime.Data.ValueObject.CardAnim;
using Runtime.Utilities;
using UnityEngine;

namespace Runtime.Strategy
{
    public class NormalCardAnimationStrategy : BaseCardAnimationStrategy
    {
        public NormalCardAnimationStrategy(Transform cardVisualTransform, CardAnimationData data)
            : base(cardVisualTransform, data)
        {
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