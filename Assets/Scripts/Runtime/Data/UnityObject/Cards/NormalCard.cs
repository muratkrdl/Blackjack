using Runtime.Abstracts.Classes;
using Runtime.Managers;
using UnityEngine;

namespace Runtime.Data.UnityObject.Cards
{
    [CreateAssetMenu(fileName = "NormalCard", menuName = "Cards/NormalCard")]
    public class NormalCard : Card
    {
        public override void DrawCard(BaseHandManager baseHandManager)
        {
            // TODO : Increase Player Card Value
            baseHandManager.IncreaseScore(CardValue);
        }
        public override void DiscardCard(BaseHandManager baseHandManager)
        {
            // TODO : Decrease Player Card Value
            baseHandManager.DecreaseScore(CardValue);
        }
    }
}