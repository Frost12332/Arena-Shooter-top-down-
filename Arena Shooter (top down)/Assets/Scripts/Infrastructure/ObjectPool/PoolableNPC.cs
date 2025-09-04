using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Infrastructure.ObjectPool
{
    public class PoolableNPC : Poolable
    {
        [SerializeField] private NavMeshAgent _agent;

        public override void Activate(Vector3 activationPosition)
        {
            _agent.Warp(activationPosition);
            
            _agent.isStopped = true;

            gameObject.SetActive(true);
        }

        protected override void Deactivate()
        {
            _agent.isStopped = true;

            gameObject.SetActive(false);
        }
    }
}