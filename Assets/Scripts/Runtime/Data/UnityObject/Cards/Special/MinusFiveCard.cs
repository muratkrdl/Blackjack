using Runtime.Abstracts.Classes;
using Runtime.Events;
using Runtime.Keys;
using UnityEngine;

namespace Runtime.Data.UnityObject.Cards.Special
{
    [CreateAssetMenu(fileName = "DoubleTrouble", menuName = "Cards/MinusFiveCard")]
    public class MinusFiveCard : SpecialCard
    {
        public override void PlayCard(BaseHandManager targetBaseHand)
        {
            // TODO : Decrease Player Score 5
            // TODO : Add Ghost Card Player
            CoreGameEvents.Instance.OnDrawCardFromBoard?.Invoke(new DrawCardParams()
            {
                BaseHandManager = targetBaseHand,
                // Type = DrawCardTypes.Normal
            });
        }

        public override void DrawCard(BaseHandManager baseHandManager)
        {
            
        }

        public override void DiscardCard(BaseHandManager baseHandManager)
        {
            
        }
    }
}