using System;
using UnityEngine;

namespace Assets.Scripts.GameLogic.Enemy.CustomAnimatorContol.Minion
{
    public class MinionAnimator : MonoBehaviour, IMoveAnimator, IMeleeAtackAnimator
    {
        private const float _stopSpeed = 0.0f;

        [SerializeField] private Animator _animator;

        private readonly int Speed = Animator.StringToHash("Speed");
        private readonly int MeleeAtack = Animator.StringToHash("MeleeAtack");
        private readonly int Hit = Animator.StringToHash("Hit");
        private readonly int Die = Animator.StringToHash("Die");

        public event Action OnMeleeAttack;
        public event Action OnMeleeAttackEnd;

        public void PlayMove(float speed)
        {
            _animator.SetFloat(Speed, speed);
        }

        public void PlayStopMove()
        {
            _animator.SetFloat(Speed, _stopSpeed);
        }



        public void PlayMeleeAttack()
        {
            _animator.SetTrigger(MeleeAtack);
        }

        public void MeleeAttackAnimationEvent() =>
            OnMeleeAttack?.Invoke();

        public void MeleeAttackEndEvent() =>
            OnMeleeAttackEnd?.Invoke();



        /*this need update*/

        public void PlayHit()
        {
            _animator.SetTrigger(Hit);
        }

        public void PlayDie()
        {
            _animator.SetBool(Die, true);
        }

        
        /*this need update*/





    }
}