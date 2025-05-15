using Runtime.Extensions;
using Runtime.Keys;
using UnityEngine.Events;

namespace Runtime.Events
{
    public class CoreGameEvents : Monosingleton<CoreGameEvents>
    {
        public UnityAction onPlayerGetCard;
        public UnityAction onEnemyGetCard;
        public UnityAction onPlayerLose;
        public UnityAction onReset;
        public UnityAction onPlayerPass;

        public UnityAction<CardMoveParams> onCardMove;

    }
}