using System;

namespace Assets.Scripts.GameLogic.Enemy.CustomAnimatorContol
{
    public interface ISpellCastShootAnimator
    {
        public event Action OnSpellCastShoot;
        public event Action OnSpellCastShootEnd;

        public void PlaySpellCastShoot();

        public void SpellCastShootAnimationEvent();

        public void CallSpellCastShootEndEvent();
    }
}