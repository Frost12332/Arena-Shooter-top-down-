using UnityEngine;

namespace Assets.Scripts.GameLogic.Enemy.CustomAnimatorContol.Mage
{
    public class SpellCastHealBehaviour : StateMachineBehaviour
    {
        MageAnimator animatorController = null;

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (animatorController == null)
                animatorController = animator.GetComponent<MageAnimator>();

            animatorController.CallSpellCastHealEndEvent();
        }
    }
}