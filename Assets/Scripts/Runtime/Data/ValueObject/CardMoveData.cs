using System;
using DG.Tweening;

namespace Runtime.Data.ValueObject
{
    [Serializable]
    public struct CardMoveData
    {
        public Ease EaseMode;
        public float Duration;
    }
}