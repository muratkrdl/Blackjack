using Runtime.Enums;
using Runtime.Managers;
using UnityEngine;

namespace Runtime.Abstracts.Classes
{
    public abstract class Card : ScriptableObject
    {
        public CardTypes Type;
        public int CardValue;
        public string CardName;
        public Sprite CardImage;
        public Sprite CardBackImage;
        public string Description;

        public abstract void DrawCard(HandManager handManager);
        public abstract void DiscardCard(HandManager handManager);
    }
}