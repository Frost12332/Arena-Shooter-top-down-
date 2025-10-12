using Assets.Scripts.GameLogic.Enemy.CustomAnimatorContol.Mage;
using UnityEngine;

namespace Assets.Scripts.GameLogic.AnimatorLogic.AnimatorContracts.Behaviour
{
    public class SpellCastSummonBehaviour : StateMachineBehaviour
    {
        ISpellCastSummonAnimator animatorController = null;

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (animatorController == null)
                animatorController = animator.GetComponent<ISpellCastSummonAnimator>();

            animatorController.CallSpellCastSummonEndEvent();
        }
    }
}