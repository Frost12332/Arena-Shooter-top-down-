using UnityEngine;

namespace Assets.Scripts.Infrastructure.ObjectPool
{
    public interface IGameObjectPool
    {
        Poolable GetObject(string id);
        void ReleaseObject(string id, Poolable gameObject);
    }
}