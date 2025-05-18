using Runtime.Abstracts.Classes;
using Runtime.Managers;
using UnityEngine;

namespace Runtime.Data.UnityObject.Cards
{
    [CreateAssetMenu(fileName = "NormalCard", menuName = "Cards/NormalCard")]
    public class NormalCard : Card
    {
        public override void DrawCard(HandManager handManager)
        {
            // TODO : Increase Player Card Value
            handManager.IncreaseScore(CardValue);
        }
        public override void DiscardCard(HandManager handManager)
        {
            // TODO : Decrease Player Card Value
            handManager.DecreaseScore(CardValue);
        }
    }
}