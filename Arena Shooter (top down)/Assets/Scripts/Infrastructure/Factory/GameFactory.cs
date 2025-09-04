using Assets.Scripts.Config;
using Assets.Scripts.Infrastructure.ObjectPool;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private PoolObjectCollection _poolObjectCollection;
        private DiContainer _diContainer;

        public GameFactory(PoolObjectCollection poolObjectCollection, DiContainer diContainer)
        {
            _poolObjectCollection = poolObjectCollection;
            _diContainer = diContainer;
        }

        public Poolable CreateObject(string id)
        {
            foreach (PoolObjectTemplate poolObjectTemplate in _poolObjectCollection.PoolCollection)
            {
                if (poolObjectTemplate.Id == id)
                {
                    GameObject tmp = _diContainer.InstantiatePrefab(poolObjectTemplate.PoolObject);
                    Poolable poolObject = tmp.GetComponent<Poolable>();
                    poolObject.Initialize(id);

                    return poolObject;
                }
            }

            Debug.LogError("Cant find pool object with id:" + id);
            return null;
        }
    }
}