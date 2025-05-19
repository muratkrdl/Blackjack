using System;
using DG.Tweening;
using Runtime.Utilities;
using UnityEngine;

namespace Runtime.Data.ValueObject.CardAnim
{
    [Serializable]
    public struct CardAnimationRotationData
    {
        public Vector3 BaseTarget;
        public Vector3 SpecialTargetFirst;
        public Vector3 SpecialTargetSecond;
        
        public float Duration;
        public Ease EaseMode;
    }
}