using UnityEngine;

namespace Assets.Scripts.Infrastructure.ObjectPool
{
    public class PoolableVFX : Poolable
    {
        public override void Activate(IPoolActivationData data)
        {
            if (data is PositionData positionData)
            {
                gameObject.transform.position = positionData.Position;

                gameObject.SetActive(true);
            }
        }

        protected override void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}