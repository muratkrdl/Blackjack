using System;

namespace Runtime.Data.ValueObject
{
    [Serializable]
    public struct GameSettingsData
    {
        public int InitialTargetScore;
        public int MaxNormalCard;
        
        //public int startingMoney = 1000
        //public int minBet = 10;
        //public int maxBet = 1000;
    }
}