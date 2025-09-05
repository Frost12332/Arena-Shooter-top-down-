using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Infrastructure.ObjectPool
{
    public class PoolableNPC : Poolable
    {
        [SerializeField] private NavMeshAgent _agent;

        public override void Activate(IPoolActivationData data)
        {
            if (data is PositionData positionData)
            {
                _agent.Warp(positionData.Position);

                gameObject.SetActive(true);
            }
        }

        protected override void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}