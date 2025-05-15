using Runtime.Abstracts.Interfaces;
using Runtime.Managers;
using UnityEngine;

namespace Runtime.Abstracts.Classes
{
    public abstract class Card : ScriptableObject, IPlayCardCommand
    {
        public string CardName;
        public Sprite CardImage;
        public string Description;

        public abstract void PlayCard(PlayerManager targetPlayer);
    }
}