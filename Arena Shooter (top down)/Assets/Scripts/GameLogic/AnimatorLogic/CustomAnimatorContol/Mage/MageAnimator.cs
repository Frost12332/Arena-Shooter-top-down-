using Assets.Scripts.GameLogic.AnimatorLogic.AnimatorContracts;
using System;
using UnityEngine;

namespace Assets.Scripts.GameLogic.Enemy.CustomAnimatorContol.Mage
{
    public class MageAnimator : MonoBehaviour, IMoveAnimator, ISpellCastShootAnimator, 
        ISpellCastSummonAnimator, ISpellCastHealAnimator, IHitAnimator
    {
        private const float _stopSpeed = 0.0f;

        [SerializeField] private Animator _animator;

        private readonly int Speed = Animator.StringToHash("Speed");
        private readonly int SpellcastShoot = Animator.StringToHash("SpellcastShoot");
        private readonly int Summon = Animator.StringToHash("Summon");
        private readonly int PowerfulSpelcastShoot = Animator.StringToHash("PowerfulSpelcastShoot");
        private readonly int Heal = Animator.StringToHash("Heal");
        private readonly int Hit = Animator.StringToHash("Hit");
        private readonly int Die = Animator.StringToHash("Die");


        /*Animation Event*/
        public event Action OnSpellCastShoot;
        public event Action OnSpellCastSummon;
        /*Animation Event*/

        /*StateMachineBehaviour*/
        public event Action OnSpellCastShootEnd;
        public event Action OnSpellCastSummonEnd;
        public event Action OnSpellCastHealEnd;
        //public event Action OnHitEnd;
        /*StateMachineBehaviour*/



        public void PlayMove(float speed)
        {
            _animator.SetFloat(Speed, speed);
        }

        public void PlayStopMove()
        {
            _animator.SetFloat(Speed, _stopSpeed);
        }

        public void PlaySpellCastShoot()
        {
            _animator.SetTrigger(SpellcastShoot);
        }

        public void PlaySummon()
        {
            _animator.SetTrigger(Summon);
        }

        public void PlayPowerfulSpelcastShoot()
        {
            _animator.SetTrigger(PowerfulSpelcastShoot);
        }

        public void StartPlayHeal()
        {
            _animator.SetBool(Heal, true);
        }

        public void StopPlayHeal()
        {
            _animator.SetBool(Heal, false);
        }

        public void PlayHit()
        {
            _animator.SetTrigger(Hit);
        }




        /*this need update*/


        public void PlayDie()
        {
            _animator.SetBool(Die, true);
        }

        /*this need update*/





        /*this is ANIMATION EVENT look at animation on Animator*/

        public void SpellCastShootAnimationEvent() =>
            OnSpellCastShoot?.Invoke();

        public void SpellCastSummonAnimationEvent() =>
            OnSpellCastSummon?.Invoke();

        /*this is ANIMATION EVENT look at animation on Animator*/





        /*this event raise from StateMachineBehaviour which attach to animation on Animator*/

        public void CallSpellCastShootEndEvent() =>
            OnSpellCastShootEnd?.Invoke();

        public void CallSpellCastSummonEndEvent() =>
            OnSpellCastSummonEnd?.Invoke();

        public void CallSpellCastHealEndEvent()
        {
            StopPlayHeal();
            OnSpellCastHealEnd?.Invoke();
        }

        //public void CallHitEndEvent() => 
        //    OnHitEnd?.Invoke();








        /*this event raise from StateMachineBehaviour which attach to animation on Animator*/
    }
}