using Assets.Scripts.Config;
using Assets.Scripts.Infrastructure.ObjectPool;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.GameLogic
{
    public class EnemySpawner : IEnemySpawner
    {
        private readonly SpawnerConfig _spawnConfig;
        private readonly DiContainer _container;
        private readonly GameObject _playerCharacter;
        private int _currentEnemyCount = 0;

        private PoolObjectCollection _poolObjectCollection;
        IGameObjectPool _gameObjectPool;

        public EnemySpawner(SpawnerConfig spawnConfig, DiContainer container, IPlayerController playerController,
            PoolObjectCollection poolObjectCollection, IGameObjectPool gameObjectPool)
        {
            _spawnConfig = spawnConfig;
            _container = container;
            _playerCharacter = playerController.GetPlayerCharacter();
            _poolObjectCollection = poolObjectCollection;
            _gameObjectPool = gameObjectPool;
        }

        public void SpawnWave(int waveSize)
        {
            for (int i = 0; i < waveSize; i++)
            {
                if (_currentEnemyCount < _spawnConfig.MaxTotalEnemies)
                {
                    if (TryFindValidSpawnPoint(out Vector3 spawnPoint))
                    {
                        SpawnEnemy(spawnPoint);
                        _currentEnemyCount++;
                    }
                }
            }

        }

        public bool TryFindValidSpawnPoint(out Vector3 spawnPoint)
        {
            for (int i = 0; i < 30; i++)
            {
                Vector2 randomCircle = Random.insideUnitCircle.normalized;
                Vector3 randomPoint = _playerCharacter.transform.position + new Vector3(randomCircle.x, 0, randomCircle.y) * _spawnConfig.SpawnRadius;

                if (Vector3.Distance(randomPoint, _playerCharacter.transform.position) >= _spawnConfig.SpawnCenterOffset)
                {
                    if (Physics.Raycast(randomPoint + Vector3.up * 50, Vector3.down, out RaycastHit hitInfo, 100f, _spawnConfig.GroundLayerMask))
                    {
                        Vector3 potentialPoint = hitInfo.point;

                        if (!Physics.CheckSphere(potentialPoint, _spawnConfig.CheckSphereRadius, _spawnConfig.ObstacleLayerMask))
                        {
                            spawnPoint = potentialPoint;
                            return true;
                        }
                    }
                }
            }

            spawnPoint = Vector3.zero;
            return false;
        }

        private void SpawnEnemy(Vector3 position)
        {
            PoolObjectTemplate enemyObject = GetRandomEnemy();

            GameObject enemyInstance = _gameObjectPool.GetObject(enemyObject.Id);

            enemyInstance.transform.position = position;

            enemyInstance.transform.Rotate(0, Random.Range(0, 360), 0);
        }

        public void OnEnemyDied()
        {
            _currentEnemyCount--;
        }

        private PoolObjectTemplate GetRandomEnemy()
        {
            int random_value = Random.Range(0, _poolObjectCollection.PoolCollection.Count - 1);
            return _poolObjectCollection.PoolCollection[random_value];
        }
    }
}