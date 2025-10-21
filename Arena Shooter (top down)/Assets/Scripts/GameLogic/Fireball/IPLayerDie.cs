using System;

namespace Assets.Scripts.GameLogic.FireballBehaviour
{
    public interface IPLayerDie
    {
        public event Action OnPlayerDie;
    }
}