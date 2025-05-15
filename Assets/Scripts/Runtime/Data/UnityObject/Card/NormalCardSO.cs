using UnityEngine;

namespace Runtime.Data.UnityObject.Card
{
    [CreateAssetMenu(fileName = "NormalCardSO", menuName = "SO/NormalCardSO", order = 0)]
    public class NormalCardSO : ScriptableObject
    {
        public Sprite CardSprite;
        public int CardValue;
    }
}