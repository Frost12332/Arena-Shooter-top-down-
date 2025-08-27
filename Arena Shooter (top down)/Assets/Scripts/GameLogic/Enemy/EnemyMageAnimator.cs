using UnityEngine;

namespace Assets.Scripts.GameLogic.Enemy
{
    public class EnemyMageAnimator : MonoBehaviour
    {
        private const float _stopSpeed = 0.0f;

        [SerializeField] private Animator _animator;

        private readonly int Speed = Animator.StringToHash("Speed");
        private readonly int SpellcastShoot = Animator.StringToHash("SpellcastShoot");
        private readonly int RaiseWarriors = Animator.StringToHash("RaiseWarriors");
        private readonly int PowerfulSpelcastShoot = Animator.StringToHash("PowerfulSpelcastShoot");
        private readonly int Spellcasting = Animator.StringToHash("Spellcasting");
        private readonly int Hit = Animator.StringToHash("Hit");
        private readonly int Die = Animator.StringToHash("Die");

        public void PlayMove(float speed)
        {
            _animator.SetFloat(Speed, speed);
        }

        public void PlayStop()
        {
            _animator.SetFloat(Speed, _stopSpeed);
        }

        public void PlaySpellCastShoot()
        {
            _animator.SetTrigger(SpellcastShoot);
        }

        public void PlayRaiseWarriors()
        {
            _animator.SetTrigger(RaiseWarriors);
        }

        public void PlayPowerfulSpelcastShoot()
        {
            _animator.SetTrigger(PowerfulSpelcastShoot);
        }

        public void PlaySpellcasting()
        {
            _animator.SetTrigger(Spellcasting);
        }

        public void PlayHit()
        {
            _animator.SetTrigger(Hit);
        }

        public void PlayDie()
        {
            _animator.SetTrigger(Die);
        }
    }
}