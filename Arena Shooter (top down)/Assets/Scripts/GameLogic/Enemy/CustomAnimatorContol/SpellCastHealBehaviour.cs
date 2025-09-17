using UnityEngine;

namespace Assets.Scripts.GameLogic.Enemy.CustomAnimatorContol
{
    public class SpellCastHealBehaviour : StateMachineBehaviour
    {
        EnemyMageAnimator animatorController = null;

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (animatorController == null)
                animatorController = animator.GetComponent<EnemyMageAnimator>();

            animatorController.CallSpellCastHealEndEvent();
        }
    }
}