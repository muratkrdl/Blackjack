using Runtime.Abstracts.Classes;

namespace Runtime.States
{
    public interface ITurnState
    {
        void OnPass(BaseHandManager baseHand);
        void OnDrawCard(BaseHandManager baseHand);
    }
} 