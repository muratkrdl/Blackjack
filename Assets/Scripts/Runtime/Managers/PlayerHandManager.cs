using Runtime.Abstracts.Classes;
using Runtime.Enums;
using Runtime.Events;
using Runtime.Keys;

namespace Runtime.Managers
{
    public class PlayerHandManager : HandManager
    {
        protected override void SubscribeEvents()
        {
            base.SubscribeEvents();
            CoreGameEvents.Instance.OnPlayerPlayCard += OnPlayerPlayCard;
        }

        protected override void UnSubscribeEvents()
        {
            base.UnSubscribeEvents();
            CoreGameEvents.Instance.OnPlayerPlayCard -= OnPlayerPlayCard;
        }

        private void OnPlayerPlayCard(PlayCardParams param)
        {
            
        }

        private void PlayNormalCard(CardObject card)
        {
            card.PlayCard(this);
            _handNormalCards.Remove(card);
        }
    }
} 