using UnityEngine;

namespace Assets.Scripts.Config.Pool.Price
{
    [CreateAssetMenu(fileName = "PoolObjectPrice", menuName = "Config/PoolObject/Price/PoolObjectPrice")]
    public class PoolObjectPrice : ScriptableObject
    {
        [SerializeField] private int _price;
        [SerializeField] private PoolObjectTemplate _poolObjectTemplate;

        public int Price => _price;
        public PoolObjectTemplate PoolObjectTemplate => _poolObjectTemplate;
    }
}