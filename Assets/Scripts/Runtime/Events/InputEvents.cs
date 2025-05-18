using Runtime.Extensions;
using UnityEngine.Events;

namespace Runtime.Events
{
    public class InputEvents : Monosingleton<InputEvents>
    {
        public UnityAction OnNormalCardDraw;
        public UnityAction OnSpecialCardDraw;
    }
}