using Runtime.Abstracts.Classes;
using Runtime.Managers;
using UnityEngine;

namespace Runtime.Data.UnityObject.Cards
{
    [CreateAssetMenu(fileName = "NormalCard", menuName = "Cards/NormalCard")]
    public class NormalCard : Card
    {
        public override void DrawCard(PlayerManager playerManager)
        {
            // TODO : Increase Player Card Value
            playerManager.IncreaseScore(CardValue);
        }
        public override void DiscardCard(PlayerManager playerManager)
        {
            // TODO : Decrease Player Card Value
            playerManager.DecreaseScore(CardValue);
        }
    }
}