using Assets.Scripts.Infrastructure.ObjectPool;
using System;
using UnityEngine;

namespace Assets.Scripts.GameLogic.FireballBehaviour
{
    public class Health : MonoBehaviour, IReleasable
    {
        [SerializeField] private int _healthPoints;
        [SerializeField] private int _maxHealthPoints;

        private bool _isAlive = true;

        public event Action OnReleased;

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

        private void Die()
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