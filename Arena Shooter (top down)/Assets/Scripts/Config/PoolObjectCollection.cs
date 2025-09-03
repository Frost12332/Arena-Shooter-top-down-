using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Config
{
    [CreateAssetMenu(fileName = "PoolCollection", menuName = "Config/PoolObject/PoolObjectCollection")]
    public class PoolObjectCollection : ScriptableObject
    {
        [SerializeField] private List<PoolObjectTemplate> _poolCollection;

        public IReadOnlyList<PoolObjectTemplate> PoolCollection => _poolCollection;
    }
}