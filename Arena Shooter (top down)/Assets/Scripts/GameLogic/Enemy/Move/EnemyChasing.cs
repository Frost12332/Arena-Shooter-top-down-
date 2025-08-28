using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Assets.Scripts.GameLogic.Enemy.Move
{
    public class EnemyChasing : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;

        private IPlayerController _playerController;

        public float minDistanceToTarget = 1.0f;

        [Inject]
        private void Construct(IPlayerController playerController)
        {
            _playerController = playerController;
        }

        private void Update()
        {
            if (Vector3.Distance(gameObject.transform.position, _playerController.GetPlayerCharacter().transform.position) >= minDistanceToTarget)
                _navMeshAgent.SetDestination(_playerController.GetPlayerCharacter().transform.position);
        }
    }
}