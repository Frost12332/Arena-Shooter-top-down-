using Assets.Scripts.GameLogic.AnimatorLogic.AnimatorContracts;
using Assets.Scripts.Infrastructure.ObjectPool;
using System;
using UnityEngine;

namespace Assets.Scripts.GameLogic.FireballBehaviour
{
    public class Health : MonoBehaviour, IReleasable
    {
        [SerializeField] protected int _healthPoints;
        [SerializeField] protected int _maxHealthPoints;

        private IHitAnimator _hitAnimator;

        private bool _isAlive = true;

        public event Action OnReleased;


        private void Start()
        {
            _hitAnimator = GetComponent<IHitAnimator>();
        }


        public void Arise()
        {
            _healthPoints = _maxHealthPoints;
            _isAlive = true;
        }

        public void Healing(int healthPoints)
        {
            if (_healthPoints + healthPoints > _maxHealthPoints)
                _healthPoints = _maxHealthPoints;
            else
                _healthPoints += healthPoints;
        }

        public void TakeDamage(int damage)
        {
            _hitAnimator.PlayHit();

            _healthPoints -= damage;

            if (_healthPoints <= 0)
                Die();
        }
        [ContextMenu("Kill")]
        public void Kill()
        {
            _healthPoints = 0;
            Die();
        }

        protected virtual void Die()
        {
            _isAlive = false;
            OnReleased?.Invoke();
        }


        public bool IsNeedHealing() =>
            _healthPoints < _maxHealthPoints && _isAlive;

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Detect collision");
        }
    }
}