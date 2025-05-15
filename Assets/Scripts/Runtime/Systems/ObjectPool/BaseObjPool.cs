using Runtime.Abstracts.Interfaces;
using Runtime.Extensions;
using UnityEngine;
using UnityEngine.Pool;

namespace Runtime.Systems.ObjectPool
{
    public class BaseObjPool<T> : Monosingleton<BaseObjPool<T>> where T : MonoBehaviour, IPoolableObj<T>
    {
        [SerializeField] protected T prefab;
        [SerializeField] private int defaultSize = 10;
        [SerializeField] private int maxSize = 100;

        private ObjectPool<T> _pool;

        protected override void Awake()
        {
            base.Awake();
            _pool = new ObjectPool<T>
            (
                CreateFunc,
                OnGet,
                OnRelease,
                OnDestroyFunc,
                true,
                defaultSize,
                maxSize
            );
        }

        protected virtual T CreateFunc()
        {
            var obj = Instantiate(prefab, transform);
            obj.SetPool(_pool);
            return obj;
        }

        protected virtual void OnGet(T obj)
        {
            obj.gameObject.SetActive(true);
        }

        protected virtual void OnRelease(T obj)
        {
            obj.gameObject.SetActive(false);
        }

        protected virtual void OnDestroyFunc(T obj)
        {
            Destroy(obj.gameObject);
        }

        protected virtual void Release(T obj)
        {
            _pool.Release(obj);
        }
        
        public T Get()
        {
            return _pool.Get();
        }

    }
}