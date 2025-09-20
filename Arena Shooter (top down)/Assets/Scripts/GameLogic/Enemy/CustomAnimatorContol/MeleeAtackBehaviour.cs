using UnityEngine;

namespace Assets.Scripts.GameLogic.Enemy.CustomAnimatorContol
{
    public class MeleeAtackBehaviour : StateMachineBehaviour
    {
        IMeleeAtackAnimator animatorController = null;

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (animatorController == null)
                animatorController = animator.GetComponent<IMeleeAtackAnimator>();

            animatorController.MeleeAttackEndEvent();
        }
    }
}