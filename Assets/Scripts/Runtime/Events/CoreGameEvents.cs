using Runtime.Extensions;
using Runtime.Keys;
using Runtime.Managers;
using UnityEngine.Events;

namespace Runtime.Events
{
    public class CoreGameEvents : Monosingleton<CoreGameEvents>
    {
        public UnityAction OnGameStart;
        public UnityAction OnGameEnd;
        public UnityAction<DrawCardParams> OnDrawCard;
        public UnityAction<HandManager> OnPass;
        public UnityAction<HandManager> OnLose;
        public UnityAction OnTourStart;
        public UnityAction OnTourEnd;
        public UnityAction OnReset;
    }
}