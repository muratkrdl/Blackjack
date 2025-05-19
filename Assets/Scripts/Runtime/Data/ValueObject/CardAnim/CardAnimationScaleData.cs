using System;
using DG.Tweening;

namespace Runtime.Data.ValueObject.CardAnim
{
    [Serializable]
    public struct CardAnimationScaleData
    {
        public float Target;
        public float Duration;
        public Ease EaseMode;
    }
}