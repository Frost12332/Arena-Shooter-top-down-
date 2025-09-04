using Assets.Scripts.Infrastructure.ObjectPool;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Factory
{
    public interface IGameFactory
    {
        Poolable CreateObject(string id);
    }
}