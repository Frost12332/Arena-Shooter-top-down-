using Assets.Scripts.Config;
using Assets.Scripts.Infrastructure.Factory;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Infrastructure.ObjectPool
{
    public class GameObjectPool : IGameObjectPool
    {
        private IGameFactory _factory;

        private Dictionary<string, Stack<GameObject>> _freeUseObjects;

        public GameObjectPool(IGameFactory factory, PoolObjectCollection poolObjectCollection)
        {
            _factory = factory;
            _freeUseObjects = new Dictionary<string, Stack<GameObject>>();

            foreach (PoolObjectTemplate poolObject in poolObjectCollection.PoolCollection)
            {
                _freeUseObjects.Add(poolObject.Id, new Stack<GameObject>());
            }
        }

        public GameObject GetObject(string id)
        {
            if (_freeUseObjects.TryGetValue(id, out Stack<GameObject> stack))
            {
                if (stack.Count > 0)
                    return stack.Pop();
                else
                {
                    RequestNewObject(id);
                    return GetObject(id);
                }
            }
            else
            {
                Debug.LogError("Cant find id:" + id);
                return null;
            }
        }

        private void RequestNewObject(string id)
        {
            GameObject createdObject = _factory.CreateObject(id);

            if (createdObject != null)
            {
                if (_freeUseObjects.ContainsKey(id))
                    _freeUseObjects[id].Push(createdObject);
            }
        }
    
        
        public void ReleaseObject(string id, GameObject gameObject)
        {
            if (_freeUseObjects.ContainsKey(id))
            {
                _freeUseObjects[id].Push(gameObject);
            }
            else
                Debug.LogError("Find unreleasable GameObject:" + gameObject.name + " id:" + id);
        }    
    }
}