using Assets.Scripts.GameLogic.FireballBehaviour;
using Assets.Scripts.GameLogic.GameResource;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.ObjectPool
{
    public class PoolablePlayer : Poolable
    {
        [SerializeField] private PlayerHealth _health;
        [SerializeField] private List<ResourceStorage> _storages;

        public override void Activate(IPoolActivationData data)
        {
            _health.Arise();

            foreach(ResourceStorage storage in _storages)
                storage.Restore();

            gameObject.SetActive(true);
        }

        protected override void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}