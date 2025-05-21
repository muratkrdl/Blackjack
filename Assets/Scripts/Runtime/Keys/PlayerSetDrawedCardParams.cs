using System.Collections.Generic;
using Runtime.Abstracts.Classes;
using UnityEngine;

namespace Runtime.Keys
{
    public struct PlayerSetDrawedCardParams
    {
        public List<CardObject> Cards;
        public Transform[] Poses;
        public CardObject DrawedVisualCard;
    }
}