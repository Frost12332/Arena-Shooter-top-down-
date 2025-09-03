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

        public GameObject CreateObject(string id)
        {
            foreach (PoolObjectTemplate poolObject in _poolObjectCollection.PoolCollection)
            {
                if (poolObject.Id == id)
                {
                    GameObject tmp = _diContainer.InstantiatePrefab(poolObject.PoolObject);
                    tmp.GetComponent<Poolable>().SetId(id);
                    
                    return tmp;
                }
            }

            Debug.LogError("Cant find pool object with id:" + id);
            return null;
        }
    }
}