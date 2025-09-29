using Assets.Scripts.Config;
using Assets.Scripts.GameLogic.Enemy.CustomAnimatorContol.Mage;
using Assets.Scripts.GameLogic.FireballBehaviour;
using Assets.Scripts.Infrastructure.ObjectPool;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.GameLogic.Enemy.AI.Behaviours
{
    public class SpellAtackStrategy : BehaviourStrategy
    {
        [SerializeField] private PoolObjectTemplate _spellAtackTemplate;
        [SerializeField] private GameObject _spellAtackStartPosition;
        [SerializeField] private MageAnimator _enemyMageAnimator;

        private IGameObjectPool _gameObjectPool;

        [Inject]
        private void Construct(IGameObjectPool gameObjectPool)
        {
            _gameObjectPool = gameObjectPool;
        }

        private void Start()
        {
            _enemyMageAnimator.OnSpellCastShoot += SpellCasting;
        }

        public override bool CanExecute()
        {
            return true;//TODO: is have enough mana
        }

        protected override IEnumerator ExecuteBehavior()
        {
            _enemyMageAnimator.PlaySpellCastShoot();



            bool animationEnd = false;

            void OnAnimationEnd()
            {
                animationEnd = true;
            }



            _enemyMageAnimator.OnSpellCastShootEnd += OnAnimationEnd;

            yield return new WaitUntil(() => animationEnd);

            _enemyMageAnimator.OnSpellCastShootEnd -= OnAnimationEnd;
        }

        private void SpellCasting()
        {
            Poolable spellAtack = _gameObjectPool.GetObject(_spellAtackTemplate.Id);
            ProjectileData projectileData = new ProjectileData(_spellAtackStartPosition.transform.position, _spellAtackStartPosition.transform.forward);

            spellAtack.Activate(projectileData);
        }

        private void OnDestroy()
        {
            _enemyMageAnimator.OnSpellCastShoot -= SpellCasting;
        }
    }
}