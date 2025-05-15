using Runtime.Managers;

namespace Runtime.Abstracts.Interfaces
{
    public interface IPlayCardCommand
    {
        void PlayCard(PlayerManager targetPlayer);
    }
}