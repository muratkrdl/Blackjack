using Runtime.Abstracts.Classes;
using Runtime.Enums;
using Runtime.Extensions;
using Runtime.Keys;
using UnityEngine.Events;

namespace Runtime.Events
{
    public class CoreGameEvents : Monosingleton<CoreGameEvents>
    {
        public UnityAction OnGameStart;
        public UnityAction OnGameEnd;
        public UnityAction<DrawedCardParams> OnDrewCardToHand;
        public UnityAction<DrawCardParams> OnDrawCardFromBoard;
        public UnityAction<BaseHandManager> OnPass;
        public UnityAction<BaseHandManager> OnLose;
        public UnityAction<PlayCardParams> OnPlayerPlayCard;
        public UnityAction OnTourStart;
        public UnityAction OnTourEnd;
        public UnityAction OnReset;
        
        
        public UnityAction OnRoundStart;
        public UnityAction OnRoundEnd;
        public UnityAction<TurnState> OnTurnChanged;
    }
}