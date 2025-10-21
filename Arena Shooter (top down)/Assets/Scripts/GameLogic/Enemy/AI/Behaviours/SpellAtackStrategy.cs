using Assets.Scripts.Config.Pool;
using Assets.Scripts.GameLogic.Enemy.CustomAnimatorContol.Mage;
using Assets.Scripts.GameLogic.FireballBehaviour;
using Assets.Scripts.GameLogic.GameResource;
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

        [SerializeField] private ResourceStorage _resourceStorage;
        [SerializeField] private float _cost = 5.0f;

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
            return _resourceStorage.HasEnough(_cost);
        }

        protected override IEnumerator ExecuteBehavior()
        {
            if (_resourceStorage.TrySpend(_cost))
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