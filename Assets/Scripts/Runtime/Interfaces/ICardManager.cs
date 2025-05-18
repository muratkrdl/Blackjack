using Runtime.Abstracts.Classes;
using Runtime.Enums;

namespace Runtime.Interfaces
{
    public interface IHandManager
    {
        void DrawCard(DrawCardTypes type);
        int GetCardsInHand();
        void PlaySpecialCard(SpecialCard card);
        int GetCurrentScore();
        void IncreaseScore(int value);
        void DecreaseScore(int value);
    }
} 