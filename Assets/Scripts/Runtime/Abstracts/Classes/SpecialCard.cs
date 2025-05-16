using Runtime.Abstracts.Interfaces;
using Runtime.Enums;
using Runtime.Managers;

namespace Runtime.Abstracts.Classes
{
    public abstract class SpecialCard : Card
    {
        public abstract void PlayCard(PlayerManager targetPlayer);
    }
}