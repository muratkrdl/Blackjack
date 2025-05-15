using Runtime.Abstracts.Classes;
using Runtime.Abstracts.Interfaces;
using Runtime.Managers;
using UnityEngine;

namespace Runtime.Data.UnityObject.Cards
{
    [CreateAssetMenu(fileName = "NormalCard", menuName = "Cards/NormalCard")]
    public class NormalCard : Card, IUndoCardCommand
    {
        public int CardValue;
        
        public override void PlayCard(PlayerManager targetPlayer)
        {
            // TODO : Increase Player Card Value
            targetPlayer.IncreaseScore(CardValue);
        }

        public void Undo(PlayerManager targetPlayer)
        {
            // TODO : Decrease Player Card Value
            targetPlayer.DecreaseScore(CardValue);
        }
    }
}