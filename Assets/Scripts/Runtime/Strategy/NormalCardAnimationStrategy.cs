using Runtime.Abstracts.Classes;
using Runtime.Data.ValueObject.CardAnim;
using UnityEngine;

namespace Runtime.Strategy
{
    public class NormalCardAnimationStrategy : BaseCardAnimationStrategy
    {
        public NormalCardAnimationStrategy(Transform cardVisualTransform, CardAnimationData data) 
            : base(cardVisualTransform, data)
        {
            // BASE Class Constructor
        }

    }
}