using DG.Tweening;
using Runtime.Data.ValueObject;
using UnityEngine;

namespace Runtime.Controllers.Card
{
    public class CardMoveController : MonoBehaviour
    {
        private CardMoveData _data;

        public void SetData(CardMoveData data)
        {
            _data = data;
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
