using Runtime.Managers;

namespace Runtime.Abstracts.Classes
{
    public abstract class SpecialCard : Card
    {
        public abstract void PlayCard(HandManager targetHand);
    }
}