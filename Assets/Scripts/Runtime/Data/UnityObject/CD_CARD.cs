using Runtime.Abstracts.Classes;
using Runtime.Data.ValueObject;
using UnityEngine;

namespace Runtime.Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_CARD", menuName = "Data/CD_CARD", order = 0)]
    public class CD_CARD : ScriptableObject
    {
        public CardMoveData MoveData;
        public CardAnimationData AnimationData;
    }
}