using Runtime.Abstracts.Classes;

namespace Runtime.States
{
    public interface ITurnState
    {
        void OnPass(HandManager hand);
        void OnDrawCard(HandManager hand);
    }
} 