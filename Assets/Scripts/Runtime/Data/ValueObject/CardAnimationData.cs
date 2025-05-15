using System;
using DG.Tweening;
using UnityEngine;

namespace Runtime.Data.ValueObject
{
    [Serializable]
    public struct CardAnimationData
    {
        public Vector2 AnimPos;
        public float Duration;
        public Ease EaseMode;
    }
}