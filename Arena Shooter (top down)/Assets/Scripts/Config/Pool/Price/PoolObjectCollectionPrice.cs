using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Config.Pool.Price
{
    [CreateAssetMenu(fileName = "PoolCollectionPrice", menuName = "Config/PoolObject/Price/PoolCollectionPrice")]
    public class PoolObjectCollectionPrice : ScriptableObject
    {
        [SerializeField] protected List<PoolObjectPrice> _poolCollection;

        public IReadOnlyList<PoolObjectPrice> PoolCollection => _poolCollection;

        public int FindPrice(PoolObjectTemplate template) =>
            FindPrice(template.Id);

        public int FindPrice(string id)
        {
            foreach (PoolObjectPrice pool in _poolCollection)
            {
                if (pool.PoolObjectTemplate.Id == id)
                    return pool.Price;
            }

            throw new ArgumentNullException(id);
        }
    }
}