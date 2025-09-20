using Assets.Scripts.GameLogic.Enemy.CustomAnimatorContol.Mage;
using System;

namespace Assets.Scripts.GameLogic.Enemy.CustomAnimatorContol
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