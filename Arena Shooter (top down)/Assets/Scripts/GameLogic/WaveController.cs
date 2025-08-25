using Assets.Scripts.Config;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.GameLogic
{
    public class WaveController : MonoBehaviour
    {
        private IEnemySpawner _enemySpawner;
        private SpawnerConfig _spawnConfig;

        [Inject]
        private void Construct(IEnemySpawner enemySpawner, SpawnerConfig spawnConfig)
        {
            _enemySpawner = enemySpawner;
            _spawnConfig = spawnConfig;
        }

        private void Start()
        {
            StartNewWave();
        }

        private void StartNewWave()
        {
            _enemySpawner.SpawnWave(_spawnConfig.EnemiesPerWave);
        }
    }
}