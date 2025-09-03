using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Assets.Scripts.GameLogic.Enemy
{
    public class LookAtPlayer : MonoBehaviour
    {
        private const float _minimalVelocity = 0.1f;
        private IPlayerController _playerController;

        [SerializeField] private NavMeshAgent _agent;

        [Inject]
        private void Construct(IPlayerController playerController)
        {
            _playerController = playerController;
        }

        private void Update()
        {
            if (!IsMove())
            {
                Vector3 direction = _playerController.GetPlayerCharacter().transform.position - transform.position;
                direction.y = 0;
                if (direction.sqrMagnitude > 0.01f)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(
                        transform.rotation,
                        targetRotation,
                        Time.deltaTime * 5f
                    );
                }
            }
                
        }

        private bool IsMove() => _agent.velocity.magnitude > _minimalVelocity;
    }
}