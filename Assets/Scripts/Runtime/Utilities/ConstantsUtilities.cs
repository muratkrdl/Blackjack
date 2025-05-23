using UnityEngine;

namespace Runtime.Utilities
{
    public static class ConstantsUtilities
    { 
        public static readonly Camera MainCamera = Camera.main;
        
#region Vectors
        
        public static readonly Vector2 Zero2 = Vector2.zero;
        public static readonly Vector2 One2 = Vector2.one;
        
        public static readonly Vector3 Zero3 = Vector3.zero;
        public static readonly Vector3 Forward3 = Vector3.forward;
        public static readonly Vector3 One3 = Vector3.one;
        
#endregion

#region Layer

        public static readonly int AllLayers = ~0;
        public static readonly int PlayerCard = LayerMask.NameToLayer("PlayerCard");
        public static readonly int AICard = LayerMask.NameToLayer("AICard");

#endregion

#region LayerMask
        
        public static readonly LayerMask PlayerCardLayerMask = LayerMask.GetMask("PlayerCard");
        public static readonly LayerMask AICardLayerMask = LayerMask.GetMask("AICard");

#endregion
        
#region Tags

        public const string Player = "Player";
        public const string Enemy = "AI";

#endregion
            
#region Animation Hash

        // Triggers
        public static readonly int SpecialDestroy = Animator.StringToHash("SpecialDestroy");
        public static readonly int NormalDestroy = Animator.StringToHash("NormalDestroy");

        // Booleans

        // Floats

        // Ints

#endregion

    }
}