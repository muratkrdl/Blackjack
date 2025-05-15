using Runtime.Data.UnityObject;
using Runtime.Extensions;
using UnityEngine;

namespace Runtime.Managers
{
    public class DataManager : Monosingleton<DataManager>
    {
        private CD_CARD _cardData;

        protected override void Awake()
        {
            base.Awake();
            _cardData = Resources.Load<CD_CARD>("Data/Cards/CD_CARD");
        }
        
        
        
    }
}