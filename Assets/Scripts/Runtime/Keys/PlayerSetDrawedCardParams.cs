using System.Collections.Generic;
using Runtime.Objects;
using UnityEngine;

namespace Runtime.Keys
{
    public struct PlayerSetDrawedCardParams
    {
        public Stack<CardObject> Cards;
        public Transform[] Poses;
        public CardObject DrawedCard;
    }
}