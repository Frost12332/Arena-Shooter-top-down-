using Assets.Scripts.GameLogic.AnimatorLogic.AnimatorContracts;
using Assets.Scripts.GameLogic.GameEventBus;
using Assets.Scripts.Infrastructure.ObjectPool;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.GameLogic.Health
{
    public class CharacterHealth : MonoBehaviour
    {
        [SerializeField] protected int _healthPoints;
        [SerializeField] protected int _maxHealthPoints;

        private IHitAnimator _hitAnimator;

        protected bool _isAlive = true;
        protected IEventBus _eventBus;

        [Inject]
        private void Construct(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void Start()
        {
            _hitAnimator = GetComponent<IHitAnimator>();
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
        }


        public bool IsNeedHealing() =>
            _healthPoints < _maxHealthPoints && _isAlive;
    }
}