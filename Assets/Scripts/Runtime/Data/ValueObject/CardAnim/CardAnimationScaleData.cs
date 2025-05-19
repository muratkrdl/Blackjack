using System;
using DG.Tweening;

namespace Runtime.Data.ValueObject.CardAnim
{
    [Serializable]
    public struct CardAnimationScaleData
    {
        public float BaseTarget;
        public float NormalTarget;
        public float SpecialTarget;
        
        public float Duration;
        public Ease EaseMode;
    }
}