using DG.Tweening;
using Runtime.Abstracts.Classes;
using Runtime.Data.ValueObject.CardAnim;
using Runtime.Utilities;
using UnityEngine;

namespace Runtime.Strategy.CardAnimation
{
    public class SpecialCardAnimationStrategy : BaseCardAnimationStrategy
    {
        private Sequence _onPointerEnterSequence;
        private Sequence _onPointerExitSequence;
        
        public SpecialCardAnimationStrategy(Transform cardVisualTransform, CardAnimationData data, Animator animator, CardObject cardObject)
            : base(cardVisualTransform, data, animator, cardObject)
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
            _animator.SetTrigger(ConstantsUtilities.SpecialDestroy);
        }

        public override void OnPointerEnter()
        {
            KillSequence(ref _onPointerEnterSequence);

            _onPointerEnterSequence = DOTween.Sequence();
            _onPointerEnterSequence.OnPause(() => _cardObject.SetBackCardImage());

            _onPointerEnterSequence
                .Append(ScaleVisualCard(_scaleData.SpecialTarget))
                .Join(MoveVisualCard(_moveData.AnimPos))
                .Join(RotateVisualCard(_rotationData.SpecialTargetFirst).OnComplete(() => _cardObject.SetNormalCardImage()))
                .Append(RotateVisualCard(_rotationData.SpecialTargetSecond));
        }

        public override void OnPointerExit()
        {
            KillSequence(ref _onPointerExitSequence);

            if (_onPointerEnterSequence != null && _onPointerEnterSequence.IsPlaying())
            {
                _onPointerEnterSequence.Pause();
            }
            
            _onPointerExitSequence = DOTween.Sequence();
            
            _onPointerExitSequence
                .Append(ScaleVisualCard(_scaleData.BaseTarget))
                .Join(MoveVisualCard(ConstantsUtilities.Zero2));
            
            if (!_onPointerEnterSequence.IsActive())
            {
                _onPointerExitSequence
                    .Join(RotateVisualCard(_rotationData.SpecialTargetFirst).OnComplete(() => _cardObject.SetBackCardImage()))
                    .Append(RotateVisualCard(_rotationData.BaseTarget));
            }
            else
            {
                _onPointerExitSequence.Join(RotateVisualCard(_rotationData.BaseTarget));
            }
        }
        
        private void KillSequence(ref Sequence sequence)
        {
            if (sequence == null) return;
            sequence.Kill();
            sequence = null;
        }
    }
}
