using Runtime.Managers;

namespace Runtime.Abstracts.Interfaces
{
    public interface IUndoCardCommand
    {
        void Undo(HandManager  handManager);
    }
}