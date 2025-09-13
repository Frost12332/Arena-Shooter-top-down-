using Assets.Scripts.Config;
using Assets.Scripts.GameLogic.Enemy.CustomAnimatorContol;
using Assets.Scripts.Infrastructure.ObjectPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Assets.Scripts.GameLogic.Enemy
{
    public class SummonStrategy : BehaviourStrategy
    {
        [SerializeField] private EnemyMageAnimator _enemyMageAnimator;
        [SerializeField] private EnemyGroup _enemyGroup;

        [SerializeField] private Transform _spawnCenter;
        [SerializeField] private float _radius = 2;

        [SerializeField] private PoolObjectTemplate _summonObjectTemplate;
        private IGameObjectPool _gameObjectPool;

        [Inject]
        private void Construct(IGameObjectPool pool)
        {
            _gameObjectPool = pool;
        }

        private void Start()
        {
            _enemyMageAnimator.OnSpellCastSummon += Summon;
        }


        public override bool CanExecute()
        {
            return _enemyGroup.HasFreeMembership();
        }

        protected override IEnumerator ExecuteBehavior()
        {
            _enemyMageAnimator.PlaySummon();

            bool animationEnd = false;

            void OnAnimationEnd()
            {
                animationEnd = true;
            }

            _enemyMageAnimator.OnSpellCastSummonEnd += OnAnimationEnd;

            yield return new WaitUntil(() => animationEnd);

            _enemyMageAnimator.OnSpellCastSummonEnd -= OnAnimationEnd;
        }

        private void Summon()
        {
            int countSummonCharacter = _enemyGroup.CountFreeMembership();

            foreach (Vector3 point in GetSpawnPoints(_spawnCenter.position, _radius, countSummonCharacter))
            {
                /*_summonObjectTemplate this must be prefab which include character and VFX for simplest
                 first working VFX then showing character, and after this character attack*/
                Poolable spawnedCharacter = _gameObjectPool.GetObject(_summonObjectTemplate.Id);
                PositionData positionData = new PositionData(point);

                spawnedCharacter.Activate(positionData);

                _enemyGroup.AddEnemy(spawnedCharacter.gameObject);
            }
        }

        private IEnumerable<Vector3> GetSpawnPoints(Vector3 center, float radius, int count)
        {
            for (int i = 0; i < count; i++)
            {
                Vector2 circle = UnityEngine.Random.insideUnitCircle * radius;
                yield return new Vector3(center.x + circle.x, center.y, center.z + circle.y);
            }
        }

        private void OnDestroy()
        {
            _enemyMageAnimator.OnSpellCastSummon -= Summon;
        }
    }
}