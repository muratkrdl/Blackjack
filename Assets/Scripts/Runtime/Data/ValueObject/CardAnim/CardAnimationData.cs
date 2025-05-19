using System;

namespace Runtime.Data.ValueObject.CardAnim
{
    [Serializable]
    public struct CardAnimationData
    {
        public CardAnimationMoveData AnimationMoveData;
        public CardAnimationScaleData AnimationScaleData;
        public CardAnimationRotationData animationRotationData;
    }
}