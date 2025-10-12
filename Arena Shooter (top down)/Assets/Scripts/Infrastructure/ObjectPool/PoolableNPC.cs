using Assets.Scripts.GameLogic.Enemy.Group;
using Assets.Scripts.GameLogic.FireballBehaviour;
using Assets.Scripts.GameLogic.GameResource;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Infrastructure.ObjectPool
{
    public class PoolableNPC : Poolable
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private EnemyMember _enemyMember;
        [SerializeField] private Health _health;
        [SerializeField] private List<ResourceStorage> _storages;
        
        private EnemyGroup _assignedGroup;


        public override void Activate(IPoolActivationData data)
        {
            if (data is NPCActivationData npcActivationData)
            {
                _agent.Warp(npcActivationData.Position);
                _assignedGroup = npcActivationData.EnemyGroup;
                _health.Arise();

                foreach (ResourceStorage storage in _storages)
                    storage.Restore();

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