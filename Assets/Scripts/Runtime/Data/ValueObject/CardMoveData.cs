using System;
using DG.Tweening;
using UnityEngine;

namespace Runtime.Data.ValueObject
{
    [Serializable]
    public struct CardMoveData
    {
        public Ease EaseMode;
        public float Duration;
    }
}