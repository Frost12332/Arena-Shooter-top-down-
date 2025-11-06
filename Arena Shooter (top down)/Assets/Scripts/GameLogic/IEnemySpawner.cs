using System;
using UnityEngine;

namespace Assets.Scripts.GameLogic
{
    public interface IEnemySpawner
    {
        event Action AllEnemyDie;

        void SpawnWave(int waveSize);
        
        //void OnEnemyDied();
        //bool TryFindValidSpawnPoint(out Vector3 spawnPoint);
    }
}