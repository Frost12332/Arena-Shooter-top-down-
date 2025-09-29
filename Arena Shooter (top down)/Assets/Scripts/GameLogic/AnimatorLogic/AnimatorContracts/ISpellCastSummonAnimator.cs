using System;

namespace Assets.Scripts.GameLogic.AnimatorLogic.AnimatorContracts
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