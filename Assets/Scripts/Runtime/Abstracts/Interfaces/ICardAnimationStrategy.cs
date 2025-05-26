using UnityEngine;

namespace Runtime.Abstracts.Interfaces
{
    public interface ICardAnimationStrategy
    {
        void OnCardSpawn();
        void OnCardDespawn();
        void OnPointerEnter();
        void OnPointerExit();
        void OnDiscard();
    }
}