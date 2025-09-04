using UnityEngine;

namespace Assets.Scripts.Infrastructure.ObjectPool
{
    public class PoolableVFX : Poolable
    {
        public override void Activate(Vector3 activationPosition)
        {
            gameObject.transform.position = activationPosition;

            gameObject.SetActive(true);
        }

        protected override void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}