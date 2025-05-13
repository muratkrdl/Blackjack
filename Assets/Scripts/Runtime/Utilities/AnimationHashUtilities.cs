using UnityEngine;

namespace Runtime.Utilities
{
    public static class AnimationHashUtilities
    {
#region Trigger

        public static readonly int Idle = Animator.StringToHash("Idle");
        public static readonly int Walk = Animator.StringToHash("Idle");
        public static readonly int Run = Animator.StringToHash("Idle");

#endregion

#region Bool

        public static readonly int IsWalking = Animator.StringToHash("IsWalking");

#endregion

#region Float

        public static readonly int Speed = Animator.StringToHash("Speed");

#endregion

#region Int

        public static readonly int AttackIndex = Animator.StringToHash("AttackIndex");

#endregion


    }
}