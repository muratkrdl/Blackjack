using Runtime.Extensions;
using UnityEngine.Events;

namespace Runtime.Events
{
    public class SpecialCardEvents : Monosingleton<SpecialCardEvents>
    {
        public UnityAction<int> onGetSpecificCard;
        public UnityAction<int> onManipulateBoardPointCard;
        public UnityAction onPutBackCard;
        public UnityAction onPutBackEnemyCard;
        public UnityAction onGetTwoChooseOneCard;
        public UnityAction onDoubleTroubleCard;
        public UnityAction onHalfPointCard;
        public UnityAction onEnemyGetCard;
        public UnityAction onGiveCardEnemyCard;
        public UnityAction onShowEnemyScoreCard;
        public UnityAction onMinusFiveCard;
        public UnityAction onBurnCard;
        public UnityAction onHideCard;
        public UnityAction onFakeCard;
        
    }
}
