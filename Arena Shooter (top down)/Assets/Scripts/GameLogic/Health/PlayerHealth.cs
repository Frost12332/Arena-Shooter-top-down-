using Assets.Scripts.GameLogic.GameEventBus;
using System;

namespace Assets.Scripts.GameLogic.Health
{
    public class PlayerHealth : CharacterHealth
    {
        protected override void Die()/*TODO: insert call EventBus.Publish<PlayerDieEventData>(_playerDieEventData)*/
        {
            base.Die();

            _eventBus.Publish<PlayerDieEventData>(new PlayerDieEventData());
        }
    }
}