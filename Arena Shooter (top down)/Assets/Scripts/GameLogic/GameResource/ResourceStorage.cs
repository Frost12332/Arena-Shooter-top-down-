using UnityEngine;

namespace Assets.Scripts.GameLogic.GameResource
{
    public class ResourceStorage : MonoBehaviour, IResourceStorage
    {
        [SerializeField] private ResourceType _resourceType;

        [SerializeField] private float _maxCountMana = 100f;
        [SerializeField] private float _currentMana;

        public ResourceType GetResourceType() =>
            _resourceType;

        public void Restore() =>
            _currentMana = _maxCountMana;

        public bool TrySpend(float amount)
        {
            if (HasEnough(amount))
            {
                _currentMana -= amount;
                return true;
            }
            return false;
        }

        public void Add(float amount) =>
            _currentMana = Mathf.Min(_maxCountMana, _currentMana + amount);

        public bool HasEnough(float amount) =>
            _currentMana >= amount;
    }
}