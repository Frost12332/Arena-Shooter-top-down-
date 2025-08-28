using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Assets.Scripts.GameLogic.Enemy.Move
{
    public class EnemyRetreating : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        private IPlayerController _playerController;

        public float minDistanceToTarget = 50.0f;
        public float minDistanceForMove = 5.0f;

        [Inject]
        private void Construct(IPlayerController playerController)
        {
            _playerController = playerController;
        }

        private void Update()
        {
            GameObject target = _playerController.GetPlayerCharacter();
            Vector3 targetPosition;

            if (IsEnemyCloseToTarget(target))
            {
                targetPosition = CalculateTargetPosition(target);
                _navMeshAgent.SetDestination(targetPosition);
            }
        }

        private bool IsEnemyCloseToTarget(GameObject target) =>
            Vector3.Distance(gameObject.transform.position, target.transform.position) < minDistanceToTarget;

        private Vector3 CalculateTargetPosition(GameObject target)
        {
            Vector3 direction = gameObject.transform.position - target.transform.position;

            direction.Normalize();

            direction *= minDistanceForMove;

            return gameObject.transform.position + direction;
        }
    }
}