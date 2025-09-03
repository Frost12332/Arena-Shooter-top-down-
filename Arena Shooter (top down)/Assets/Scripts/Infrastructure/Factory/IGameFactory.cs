using UnityEngine;

namespace Assets.Scripts.Infrastructure.Factory
{
    public interface IGameFactory
    {
        GameObject CreateObject(string id);
    }
}