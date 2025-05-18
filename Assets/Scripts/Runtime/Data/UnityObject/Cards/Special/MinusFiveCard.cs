using Runtime.Abstracts.Classes;
using Runtime.Events;
using Runtime.Keys;
using Runtime.Managers;
using UnityEngine;

namespace Runtime.Data.UnityObject.Cards.Special
{
    [CreateAssetMenu(fileName = "DoubleTrouble", menuName = "Cards/MinusFiveCard")]
    public class MinusFiveCard : SpecialCard
    {
        public override void PlayCard(HandManager targetHand)
        {
            // TODO : Decrease Player Score 5
            // TODO : Add Ghost Card Player
            CoreGameEvents.Instance.OnDrawCard?.Invoke(new DrawCardParams()
            {
                // Obj = ,
                HandManager = targetHand
            });
        }

        public override void DrawCard(HandManager handManager)
        {
            
        }

        public override void DiscardCard(HandManager handManager)
        {
            
        }
    }
}