using System;

namespace Assets.Scripts.GameLogic.Enemy.CustomAnimatorContol
{
    public interface ISpellCastHealAnimator
    {
        public event Action OnSpellCastHealEnd;

        public void StartPlayHeal();

        public void CallSpellCastHealEndEvent();
    }
}