using UnityEngine;

namespace Runtime.Utilities
{
    public static class ConstantsUtilities
    {

#region GameObject

        public static readonly Camera MainCamera = Camera.main;

#endregion

#region Vectors
        
        public static readonly Vector2 Zero2 = Vector2.zero;
        public static readonly Vector2 One2 = Vector2.one;
        
        public static readonly Vector3 Zero3 = Vector3.zero;
        public static readonly Vector3 One3 = Vector3.one;
        public static readonly Vector3 Forward3 = Vector3.forward;
        
#endregion

#region Layer

        public static readonly int AllLayers = ~0;
        public static readonly int InteractableLayer = LayerMask.NameToLayer("Interactable");
        public static readonly int UnInteractableLayer = LayerMask.NameToLayer("UnInteractable");

#endregion

#region LayerMask
        
        public static readonly LayerMask InteractableLayerMask = LayerMask.GetMask("Interactable");
        public static readonly LayerMask UnInteractableLayerMask = LayerMask.GetMask("UnInteractable");

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