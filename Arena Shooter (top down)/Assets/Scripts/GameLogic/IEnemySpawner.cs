using UnityEngine;

namespace Assets.Scripts.GameLogic
{
    public interface IEnemySpawner
    {
        void OnEnemyDied();
        void SpawnWave(int waveSize);
        bool TryFindValidSpawnPoint(out Vector3 spawnPoint);
    }
}