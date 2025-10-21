using UnityEngine;

namespace Assets.Scripts.Config.Pool
{
    [CreateAssetMenu(fileName = "PoolObjectTemplate", menuName = "Config/PoolObject/PoolObjectTemplate")]
    public class PoolObjectTemplate : ScriptableObject
    {
        [SerializeField] private string _id;
        [SerializeField] private GameObject _poolObject;

        public string Id => _id;
        public GameObject PoolObject => _poolObject;
    }
}