using System;

namespace Assets.Scripts.GameLogic.Enemy.CustomAnimatorContol
{
    public interface ISpellCastSummonAnimator
    {
        public event Action OnSpellCastSummon;
        public event Action OnSpellCastSummonEnd;

        public void PlaySummon();

        public void SpellCastSummonAnimationEvent();

        public void CallSpellCastSummonEndEvent();
    }
}