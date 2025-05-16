using Runtime.Abstracts.Interfaces;
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
        public string Description;

        public abstract void DrawCard(PlayerManager playerManager);
        public abstract void DiscardCard(PlayerManager playerManager);
    }
}