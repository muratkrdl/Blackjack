namespace Runtime.Abstracts.Interfaces
{
    public interface ICardAnimationStrategy
    {
        void OnCardSpawn();
        void OnPointerEnter();
        void OnPointerExit();
    }
}