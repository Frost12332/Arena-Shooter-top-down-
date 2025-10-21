using Assets.Scripts.Config;
using Assets.Scripts.Config.Pool;
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

        private PoolObjectCollection _enemyPoolObjectCollection;
        IGameObjectPool _gameObjectPool;

        public EnemySpawner(SpawnerConfig spawnConfig, DiContainer container, IPlayerController playerController,
            EnemyPoolObjectCollection enemyPoolObjectCollection, IGameObjectPool gameObjectPool)
        {
            _spawnConfig = spawnConfig;
            _container = container;
            _playerCharacter = playerController.GetPlayerCharacter();
            _enemyPoolObjectCollection = enemyPoolObjectCollection;
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

        private void SpawnEnemy(Vector3 spawnPoint)
        {
            PoolObjectTemplate enemyObject = _enemyPoolObjectCollection.GetRandomPoolObject();

            Poolable enemyInstance = _gameObjectPool.GetObject(enemyObject.Id);
            NPCActivationData npcActivationData = new NPCActivationData(spawnPoint, null);

            enemyInstance.Activate(npcActivationData);
        }

        public void OnEnemyDied()
        {
            _currentEnemyCount--;
        }
    }
}