using Runtime.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Events
{
    public class InputEvents : Monosingleton<InputEvents>
    {
        public UnityAction<Vector2> OnMouseHover;
        public UnityAction OnMouseClick;
        public UnityAction OnDisableInput;
        public UnityAction OnEnableInput;
    }
}