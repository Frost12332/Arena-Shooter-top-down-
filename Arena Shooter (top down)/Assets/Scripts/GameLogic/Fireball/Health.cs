using Assets.Scripts.Infrastructure.ObjectPool;
using System;
using UnityEngine;

namespace Assets.Scripts.GameLogic.FireballBehaviour
{
    public class Health : MonoBehaviour, IReleasable
    {
        [SerializeField] private int _healthPoints;
        [SerializeField] private int _maxHealthPoints;

        public event Action OnReleased;

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
            OnReleased?.Invoke();
        }


        public bool IsNeedHealing() =>
            _healthPoints < _maxHealthPoints;
    }
}