using UnityEngine;

namespace Assets.Scripts.GameLogic.GameResource
{
    public class ResourceStorage : MonoBehaviour, IResourceStorage
    {
        [SerializeField] private ResourceType _resourceType;

        [SerializeField] private float _currentCount;
        [SerializeField] private float _maxCount = 100f;

        public ResourceType GetResourceType() =>
            _resourceType;

        public void Restore() =>
            _currentCount = _maxCount;

        public bool TrySpend(float amount)
        {
            if (HasEnough(amount))
            {
                _currentCount -= amount;
                return true;
            }
            return false;
        }

        public void Add(float amount) =>
            _currentCount = Mathf.Min(_maxCount, _currentCount + amount);

        public bool HasEnough(float amount) =>
            _currentCount >= amount;
    }
}