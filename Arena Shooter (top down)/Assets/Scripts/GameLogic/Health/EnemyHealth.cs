using Assets.Scripts.GameLogic.GameEventBus;
using Assets.Scripts.Infrastructure.ObjectPool;
using System;

namespace Assets.Scripts.GameLogic.Health
{
    public class EnemyHealth : CharacterHealth, IReleasable
    {
        public event Action OnReleased;

        public void Arise()
        {
            _healthPoints = _maxHealthPoints;
            _isAlive = true;
        }

        protected override void Die()
        {
            base.Die();

            _eventBus.Publish<EnemyDieEventData>(new EnemyDieEventData());/*insert data about enemy: mage, minion. Real or created by mage, etc...*/
            OnReleased?.Invoke();
        }
    }
}