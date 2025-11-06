using Assets.Scripts.Config;
using Assets.Scripts.Config.Pool;
using Assets.Scripts.Config.Pool.Price;
using Assets.Scripts.GameLogic.GameEventBus;
using Assets.Scripts.Infrastructure.ObjectPool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.GameLogic
{
    public class EnemySpawner : IEnemySpawner
    {
        private const int _baseCountNPC = 10;
        private const int _maxCountTryingSearchSpawnPoint = 30;

        private readonly SpawnerConfig _spawnConfig;
        private readonly DiContainer _container;
        private readonly GameObject _playerCharacter;
        private int _currentEnemyCount = 0;

        IGameObjectPool _gameObjectPool;
        private readonly PoolObjectCollection _enemyPoolObjectCollection;
        private readonly PoolObjectCollectionPrice _poolObjectCollectionPrice;
        private IEnumerator<PoolObjectPrice> _enemyEnumerator;
        private readonly IEventBus _eventBus;

        public event Action AllEnemyDie;

        public EnemySpawner(SpawnerConfig spawnConfig, DiContainer container, IPlayerController playerController,
            IGameObjectPool gameObjectPool, EnemyPoolObjectCollection enemyPoolObjectCollection,
            PoolObjectCollectionPrice poolObjectCollectionPrice, IEventBus eventBus)
        {
            _spawnConfig = spawnConfig;
            _container = container;
            _playerCharacter = playerController.GetPlayerCharacter();

            _gameObjectPool = gameObjectPool;
            _enemyPoolObjectCollection = enemyPoolObjectCollection;
            _poolObjectCollectionPrice = poolObjectCollectionPrice;

            _eventBus = eventBus;

            _eventBus.Subscribe<EnemyDieEventData>(OnEnemyDied);
        }

        public void SpawnWave(int wavePower)
        {
            PoolObjectPrice poolObjectPrice = GetLowCost();

            CreateBaseNPC(ref wavePower, poolObjectPrice);
            
            if (wavePower > 0)
                CreateAdittionalNPC(ref wavePower);
        }

        private PoolObjectPrice GetLowCost()
        {
            int price = _poolObjectCollectionPrice.PoolCollection[0].Price;
            PoolObjectPrice poolObjectPrice = _poolObjectCollectionPrice.PoolCollection[0];

            foreach (PoolObjectPrice item in _poolObjectCollectionPrice.PoolCollection)
            {
                if (item.Price < price)
                {
                    price = item.Price;
                    poolObjectPrice = item;
                }
            }

            return poolObjectPrice;
        }

        private void CreateBaseNPC(ref int wavePower, PoolObjectPrice baseNPC)
        {
            for (int i = 0; i < _baseCountNPC; i++)
            {
                if (wavePower >= baseNPC.Price)
                {
                    if (TryFindValidSpawnPoint(out Vector3 validSpawnPoint))
                    {
                        SpawnEnemy(validSpawnPoint, baseNPC.PoolObjectTemplate.Id);
                        wavePower -= baseNPC.Price;
                    }
                }
            }
        }

        private void CreateAdittionalNPC(ref int wavePower)
        {
            int index = 0;
            PoolObjectPrice poolObjectPrice;
            
            PrepareEnumerator();

            while (true)
            {
                poolObjectPrice = GetCurrentPoolObjectEnumerator();

                if (wavePower >= poolObjectPrice.Price)
                {
                    if (TryFindValidSpawnPoint(out Vector3 validSpawnPoint))
                    {
                        SpawnEnemy(validSpawnPoint, poolObjectPrice.PoolObjectTemplate.Id);
                        wavePower -= poolObjectPrice.Price;
                    }
                    index = 0;
                }
                else
                {
                    index++;

                    if (index > _poolObjectCollectionPrice.PoolCollection.Count)
                        break;
                }
            }
        }

        private PoolObjectPrice GetCurrentPoolObjectEnumerator()
        {
            _enemyEnumerator.MoveNext();
            return _enemyEnumerator.Current;
        }

        private void PrepareEnumerator()
        {
            if (_enemyEnumerator == null)
                _enemyEnumerator = GetLoopedEnumerator();
        }

        private IEnumerator<PoolObjectPrice> GetLoopedEnumerator()
        {
            while (true)
                foreach (PoolObjectPrice poolObjectPrice in _poolObjectCollectionPrice.PoolCollection)
                    yield return poolObjectPrice;
        }

        private bool TryFindValidSpawnPoint(out Vector3 spawnPoint)
        {
            for (int i = 0; i < _maxCountTryingSearchSpawnPoint; i++)
            {
                Vector2 randomCircle = UnityEngine.Random.insideUnitCircle.normalized;
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

        private void SpawnEnemy(Vector3 spawnPoint, string idEnemyTemplate)
        {
            Poolable enemyInstance = _gameObjectPool.GetObject(idEnemyTemplate);
            NPCActivationData npcActivationData = new NPCActivationData(spawnPoint, null);

            enemyInstance.Activate(npcActivationData);

            _currentEnemyCount++;
        }

        private void OnEnemyDied(EnemyDieEventData eventData)
        {
            _currentEnemyCount--;

            if (_currentEnemyCount == 0)
                AllEnemyDie?.Invoke();
        }

        ~EnemySpawner()
        {
            _eventBus.Unsubscribe<EnemyDieEventData>(OnEnemyDied);
        }
    }
}