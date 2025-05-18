using Runtime.Abstracts.Classes;
using Runtime.Abstracts.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

namespace Runtime.Objects
{
    public class FirstCardObject : CardObject, IPoolableObj<FirstCardObject>
    {
        [SerializeField] private TextMeshPro scoreText;
        
        private ObjectPool<FirstCardObject> _pool;

        public override void SetCardSoData(Card cardData)
        {
            base.SetCardSoData(cardData);
            scoreText.text = CardSoData.CardValue.ToString();
            spriteRenderer.sprite = CardSoData.CardBackImage;
        }

        public void SetPool(ObjectPool<FirstCardObject> pool)
        {
            _pool = pool;
        }

        public override void ReleasePool()
        {
            _pool.Release(this);
        }
    }
}