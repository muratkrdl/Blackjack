using UnityEngine.Pool;

namespace Runtime.Abstracts.Interfaces
{
    public interface IPoolableObj<T> where T : class
    {
        void SetPool(ObjectPool<T> pool);
    }
}
