using DG.Tweening;
using UnityEngine;

namespace Runtime.Controllers.Card
{
    public class CardMoveController : MonoBehaviour
    {
        // TODO : Flyweight
        [SerializeField] private Ease easeMode;
        [SerializeField] private float duration;
        
        public void GoPos(Vector3 pos)
        {
            transform.DOMove(pos, duration).SetEase(easeMode).OnComplete(() =>
            {
                // TODO : VFX
                
            });
        }

        
        
    }
}
