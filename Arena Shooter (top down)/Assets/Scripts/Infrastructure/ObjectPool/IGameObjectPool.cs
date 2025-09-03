using UnityEngine;

namespace Assets.Scripts.Infrastructure.ObjectPool
{
    public interface IGameObjectPool
    {
        GameObject GetObject(string id);
        void ReleaseObject(string id, GameObject gameObject);
    }
}