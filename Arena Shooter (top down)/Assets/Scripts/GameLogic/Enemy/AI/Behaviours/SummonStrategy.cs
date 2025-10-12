using Assets.Scripts.Config;
using Assets.Scripts.GameLogic.Enemy.CustomAnimatorContol.Mage;
using Assets.Scripts.GameLogic.Enemy.Group;
using Assets.Scripts.GameLogic.GameResource;
using Assets.Scripts.Infrastructure.ObjectPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Assets.Scripts.GameLogic.Enemy.AI.Behaviours
{
    public class SummonStrategy : BehaviourStrategy
    {
        [SerializeField] private MageAnimator _enemyMageAnimator;
        [SerializeField] private EnemyGroup _enemyGroup;

        [SerializeField] private Transform _spawnCenter;
        [SerializeField] private float _radius = 2;

        [SerializeField] private PoolObjectTemplate _summonObjectTemplate;
        private IGameObjectPool _gameObjectPool;

        [SerializeField] private float _cost = 10.0f;/*how much cost summon 1 character*/

        [SerializeField] private ResourceStorage _resourceStorage;

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
            return _enemyGroup.HasFreeMembership() && _resourceStorage.HasEnough(_cost);
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
                if (_resourceStorage.TrySpend(_cost))
                {
                    /*_summonObjectTemplate this must be prefab which include character and VFX for simplest
                     first working VFX then showing character, and after this character attack*/
                    Poolable spawnedCharacter = _gameObjectPool.GetObject(_summonObjectTemplate.Id);
                    NPCActivationData npcActivationData = new NPCActivationData(point, _enemyGroup);

                    spawnedCharacter.Activate(npcActivationData);
                }
            }
        }

        private IEnumerable<Vector3> GetSpawnPoints(Vector3 center, float radius, int count)
        {
            List<Vector3> points = new List<Vector3>();
            float angleStep = 360f / count;

            for (int i = 0; i < count; i++)
            {
                float angle = angleStep * i + Random.Range(-angleStep * 0.3f, angleStep * 0.3f);
                float distance = Random.Range(radius * 0.5f, radius);

                float x = Mathf.Cos(angle * Mathf.Deg2Rad) * distance;
                float z = Mathf.Sin(angle * Mathf.Deg2Rad) * distance;

                points.Add(new Vector3(center.x + x, center.y, center.z + z));
            }

            return points;
        }

        private void OnDestroy()
        {
            _enemyMageAnimator.OnSpellCastSummon -= Summon;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere(_spawnCenter.position, _radius);

            Gizmos.color = Color.white;
        }
    }
}