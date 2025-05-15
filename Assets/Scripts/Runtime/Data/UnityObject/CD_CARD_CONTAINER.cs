using Runtime.Abstracts.Classes;
using UnityEngine;

namespace Runtime.Data.UnityObject.Cards
{
    [CreateAssetMenu(fileName = "CD_CARD_CONTAINER", menuName = "Data/CD_CARD_CONTAINER", order = 0)]
    public class CD_CARD_CONTAINER : ScriptableObject
    {
        public Card[] NormalCards;
        public Card[] SpecialCards;
    }
}