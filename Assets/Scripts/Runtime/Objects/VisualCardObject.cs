using Runtime.Abstracts.Classes;
using Runtime.Abstracts.Interfaces;
using UnityEngine.Pool;

namespace Runtime.Objects
{
    public class VisualCardObject : CardObject, IPoolableObj<VisualCardObject>
    {
        private ObjectPool<VisualCardObject> _pool;

        public override void SetCardSoData(Card cardData)
        {
            base.SetCardSoData(cardData);
            spriteRenderer.sprite = CardSoData.CardImage;
        }

        public void SetPool(ObjectPool<VisualCardObject> pool)
        {
            _pool = pool;
        }

        public override void ReleasePool()
        {
            _pool.Release(this);
        }
    }
}