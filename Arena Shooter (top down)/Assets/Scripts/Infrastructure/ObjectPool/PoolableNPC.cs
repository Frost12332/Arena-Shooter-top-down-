using Assets.Scripts.GameLogic.Enemy.Group;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Infrastructure.ObjectPool
{
    public class PoolableNPC : Poolable
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private EnemyMember _enemyMember;
        
        private EnemyGroup _assignedGroup;


        public override void Activate(IPoolActivationData data)
        {
            if (data is NPCActivationData npcActivationData)
            {
                _agent.Warp(npcActivationData.Position);
                _assignedGroup = npcActivationData.EnemyGroup;

                if (_assignedGroup != null)
                {
                    _assignedGroup.Add(_enemyMember);
                }

                gameObject.SetActive(true);
            }
        }

        protected override void Deactivate()
        {
            if (_assignedGroup != null)
            {
                _assignedGroup.Remove(_enemyMember);
                _assignedGroup = null;
            }

            gameObject.SetActive(false);
        }
    }
}