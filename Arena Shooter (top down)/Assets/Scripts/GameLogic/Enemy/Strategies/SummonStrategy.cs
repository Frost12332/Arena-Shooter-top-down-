using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.GameLogic.Enemy
{
    public class SummonStrategy : BehaviourStrategy
    {
        [SerializeField] private float timeDelay = 0.4f;
        [SerializeField] private Transform _pointForSpawn;
        [SerializeField] private EnemyGroup _enemyGroup;

        [SerializeField] private GameObject _spawnEnemyVFX;
        [SerializeField] private GameObject _enemyPrefab;

        public int countSummonEnemy = 5;


        public override bool CanExecute()
        {
            return _enemyGroup.Count < countSummonEnemy;
        }

        protected override IEnumerator ExecuteBehavior()
        {
            Vector3[] spawnPoints = {
                _pointForSpawn.position,
                new Vector3(_pointForSpawn.position.x - 2, _pointForSpawn.position.y, _pointForSpawn.position.z),
                new Vector3(_pointForSpawn.position.x + 2, _pointForSpawn.position.y, _pointForSpawn.position.z),
                new Vector3(_pointForSpawn.position.x - 4, _pointForSpawn.position.y, _pointForSpawn.position.z),
                new Vector3(_pointForSpawn.position.x + 4, _pointForSpawn.position.y, _pointForSpawn.position.z),
            };

            foreach (Vector3 spawnPoint in spawnPoints)
            {
                GameObject vfx = Instantiate(_spawnEnemyVFX, spawnPoint, Quaternion.identity, null);
            }

            yield return new WaitForSeconds(timeDelay);

            foreach (Vector3 spawnPoint in spawnPoints)
            {
                GameObject spawnedEnemy = Instantiate(_enemyPrefab, spawnPoint, Quaternion.identity, null);

                spawnedEnemy.GetComponent<NavMeshAgent>().Warp(spawnPoint);

                _enemyGroup.AddEnemy(spawnedEnemy);
            }
        }
    }
}