using System;
using DG.Tweening;
using UnityEngine;

namespace Runtime.Data.ValueObject.CardAnim
{
    [Serializable]
    public struct CardAnimationMoveData
    {
        public Vector2 AnimPos;
        public float Duration;
        public Ease EaseMode;
    }
}