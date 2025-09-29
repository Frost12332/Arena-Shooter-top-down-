using System;

namespace Assets.Scripts.GameLogic.AnimatorLogic.AnimatorContracts
{
    public interface IMeleeAtackAnimator
    {
        public event Action OnMeleeAttack;
        public event Action OnMeleeAttackEnd;

        public void PlayMeleeAttack();

        public void MeleeAttackAnimationEvent();

        public void MeleeAttackEndEvent();
    }
}