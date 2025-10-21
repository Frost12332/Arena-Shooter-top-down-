using System;

namespace Assets.Scripts.GameLogic.FireballBehaviour
{
    public class PlayerHealth : Health, IPLayerDie
    {
        public event Action OnPlayerDie;

        protected override void Die()
        {
            base.Die();
            OnPlayerDie?.Invoke();
        }
    }
}