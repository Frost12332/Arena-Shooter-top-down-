using UnityEngine;

namespace Assets.Scripts.GameLogic.AnimatorLogic.AnimatorContracts.Behaviour
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