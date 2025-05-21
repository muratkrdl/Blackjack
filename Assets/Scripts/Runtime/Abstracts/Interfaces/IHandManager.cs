using Runtime.Abstracts.Classes;

namespace Runtime.Abstracts.Interfaces
{
    public interface IHandManager
    {
        void PlaySpecialCard(CardObject card);
        int GetCardsInHand();
        int GetCurrentScore();
        void IncreaseScore(int value);
        void DecreaseScore(int value);
    }
} 