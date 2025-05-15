using Runtime.Abstracts.Classes;
using Runtime.Managers;
using UnityEngine;

namespace Runtime.Data.UnityObject.Cards
{
    [CreateAssetMenu(fileName = "DoubleTrouble", menuName = "Cards/MinusFiveCard")]
    public class MinusFiveCard : SpecialCard
    {
        public override void PlayCard(PlayerManager targetPlayer)
        {
            // TODO : Decrease Player Score 5
            // TODO : Add Ghost Card Player
            
        }
    }
}