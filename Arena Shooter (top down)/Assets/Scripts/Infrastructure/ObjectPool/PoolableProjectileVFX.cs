using Assets.Scripts.GameLogic.FireballBehaviour;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.ObjectPool
{
    public class PoolableProjectileVFX : Poolable
    {
        [SerializeField] private Fireball _fireball;

        public override void Activate(IPoolActivationData data)
        {
            if (data is ProjectileData projectileData)
            {
                gameObject.transform.position = projectileData.Position;
                _fireball.Init(projectileData.Direction);

                gameObject.SetActive(true);
            }
        }

        protected override void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}