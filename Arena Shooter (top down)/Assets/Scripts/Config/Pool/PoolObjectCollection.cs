using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Config.Pool
{
    [CreateAssetMenu(fileName = "PoolCollection", menuName = "Config/PoolObject/PoolObjectCollection")]
    public class PoolObjectCollection : ScriptableObject
    {
        [SerializeField] protected List<PoolObjectTemplate> _poolCollection;

        public IReadOnlyList<PoolObjectTemplate> PoolCollection => _poolCollection;

        public PoolObjectTemplate GetRandomPoolObject() =>
            _poolCollection[Random.Range(0, _poolCollection.Count)];
    }
}