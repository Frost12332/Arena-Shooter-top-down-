using Assets.Scripts.GameLogic.Enemy.CustomAnimatorContol;
using Assets.Scripts.GameLogic.FireballBehaviour;
using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.GameLogic.Enemy.BehaviourStrategies
{
    public class MeleeAtackStrategy : BehaviourStrategy
    {
        [SerializeField] private float _attackRadius;
        [SerializeField] private float _attackDistance;
        [SerializeField] private int _attackDamage = 5;
        [SerializeField] private LayerMask _layerMask;

        private Collider[] _hits = new Collider[1];

        private IPlayerController _playerController;
        private IMeleeAtackAnimator _meleeAtackAnimator;

        

        [Inject]
        private void Construct(IPlayerController playerController)
        {
            _playerController = playerController;
        }

        private void Awake()
        {
            _meleeAtackAnimator = GetComponent<IMeleeAtackAnimator>();
        }

        private void Start()
        {
            _meleeAtackAnimator.OnMeleeAttack += MeleeAttack;
        }


        public override bool CanExecute()
        {
            return Vector3.Distance(gameObject.transform.position, _playerController.GetPlayerCharacter().transform.position) <= _attackDistance;
        }

        protected override IEnumerator ExecuteBehavior()
        {
            _meleeAtackAnimator.PlayMeleeAttack();

            bool animationEnd = false;

            void OnAnimationEnd()
            {
                animationEnd = true;
            }

            _meleeAtackAnimator.OnMeleeAttackEnd += OnAnimationEnd;

            yield return new WaitUntil(() => animationEnd);

            _meleeAtackAnimator.OnMeleeAttackEnd -= OnAnimationEnd;
        }


        private void MeleeAttack()
        {
            Vector3 attackPoint = GetAttackPoint();

            int hitAmount = Physics.OverlapSphereNonAlloc(attackPoint, _attackRadius, _hits, _layerMask);

            ProcessHits(hitAmount);
        }

        private Vector3 GetAttackPoint()
        {
            Vector3 startPoint = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);

            return startPoint + (transform.forward * _attackDistance);
        }

        private void ProcessHits(int hitAmount)
        {
            Health health;

            for (int i = 0; i < hitAmount; i++)
            {
                health = _hits[i].GetComponent<Health>();

                health.TakeDamage(_attackDamage);
            }
        }


        private void OnDestroy()
        {
            _meleeAtackAnimator.OnMeleeAttack -= MeleeAttack;
        }



        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere(GetAttackPoint(), _attackRadius);

            Gizmos.color = Color.white;
        }
    }
}