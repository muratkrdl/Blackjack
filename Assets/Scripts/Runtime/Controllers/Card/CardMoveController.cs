using DG.Tweening;
using Runtime.Data.UnityObject;
using Runtime.Data.ValueObject;
using UnityEngine;

namespace Runtime.Controllers.Card
{
    public class CardMoveController : MonoBehaviour
    {
        // TODO : Flyweight
        private CardMoveData _data;

        private void Awake()
        {
            _data = Resources.Load<CD_CARD>("Data/CD_CARD").MoveData;
        }

        public void GoPos(Vector3 pos)
        {
            transform.DOMove(pos, _data.Duration).SetEase(_data.EaseMode).OnComplete(() =>
            {
                // TODO : VFX, CardReachThePlatformEvent
                
            });
        }

        
        
    }
}
