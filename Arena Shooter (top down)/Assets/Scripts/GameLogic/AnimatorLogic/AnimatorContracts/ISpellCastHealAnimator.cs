using System;

namespace Assets.Scripts.GameLogic.AnimatorLogic.AnimatorContracts
{
    public interface ISpellCastHealAnimator
    {
        public event Action OnSpellCastHealEnd;

        public void StartPlayHeal();

        public void CallSpellCastHealEndEvent();
    }
}