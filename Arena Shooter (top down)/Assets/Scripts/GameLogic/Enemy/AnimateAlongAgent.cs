using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.GameLogic.Enemy
{
    public class AnimateAlongAgent : MonoBehaviour
    {
        private const float _minimalVelocity = 0.1f;

        private IEnemyAnimator _animator;

        [SerializeField] private NavMeshAgent _agent;

        private void Awake()
        {
            _animator = GetComponent<IEnemyAnimator>();

            if (_animator == null)
                throw new System.NullReferenceException($"IEnemyAnimator is null on: {gameObject.name}");
        }

        private void Update()
        {
            if (IsMove())
                _animator.PlayMove(_agent.velocity.magnitude);
            else
                _animator.PlayStopMove();
        }

        private bool IsMove() => _agent.velocity.magnitude > _minimalVelocity;
    }
}