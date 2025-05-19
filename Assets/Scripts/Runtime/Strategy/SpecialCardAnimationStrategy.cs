using DG.Tweening;
using Runtime.Abstracts.Classes;
using Runtime.Data.ValueObject.CardAnim;
using UnityEngine;

namespace Runtime.Strategy
{
    public class SpecialCardAnimationStrategy : BaseCardAnimationStrategy
    {
        public SpecialCardAnimationStrategy(Transform cardVisualTransform, CardAnimationData data) 
            : base(cardVisualTransform, data)
        {
            // BASE Class Constructor
        }

        public override void OnPointerEnter()
        {
            base.OnPointerEnter();
            float target = 180f;
            RotateYAxisVisualCard(90f, () =>
            {
                RotateYAxisVisualCard(target, null);
                if (Mathf.Approximately(target, 180))
                {
                    // TODO : SpriteRenderer Change To FaceSprite
                    
                }
            });
        }

        public override void OnPointerExit()
        {
            base.OnPointerExit();
            float target = 0f;
            RotateYAxisVisualCard(90f, () =>
            {
                RotateYAxisVisualCard(target, null);
                if (Mathf.Approximately(target, 0))
                {
                    // TODO : SpriteRenderer Change To BackSprite
                    
                }
            });
        }

        private void RotateYAxisVisualCard(float yAxis, TweenCallback onComplete)
        {
            Vector3 newRot = new Vector3(0, yAxis, 0);
            _cardVisualTransform.DORotate(newRot, _rotateData.Duration / 2)
                .SetEase(_rotateData.EaseMode)
                .OnComplete(onComplete);
        }

    }
}