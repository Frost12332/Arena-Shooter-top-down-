using Assets.Scripts.Config;
using Assets.Scripts.Infrastructure.Factory;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.ObjectPool
{
    public class GameObjectPool : IGameObjectPool
    {
        private IGameFactory _factory;

        private Dictionary<string, Stack<Poolable>> _freeUseObjects;

        public GameObjectPool(IGameFactory factory, PoolObjectCollection poolObjectCollection)
        {
            _factory = factory;
            _freeUseObjects = new Dictionary<string, Stack<Poolable>>();

            foreach (PoolObjectTemplate poolObject in poolObjectCollection.PoolCollection)
            {
                _freeUseObjects.Add(poolObject.Id, new Stack<Poolable>());
            }
        }

        public Poolable GetObject(string id)
        {
            if (_freeUseObjects.TryGetValue(id, out Stack<Poolable> stack))
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
            Poolable createdObject = _factory.CreateObject(id);

            if (createdObject != null)
            {
                if (_freeUseObjects.ContainsKey(id))
                    _freeUseObjects[id].Push(createdObject);
            }
        }
    
        
        public void ReleaseObject(string id, Poolable poolObject)
        {
            if (_freeUseObjects.ContainsKey(id))
            {
                _freeUseObjects[id].Push(poolObject);
            }
            else
                Debug.LogError("Find unreleasable GameObject:" + poolObject.name + " id:" + id);
        }    
    }
}