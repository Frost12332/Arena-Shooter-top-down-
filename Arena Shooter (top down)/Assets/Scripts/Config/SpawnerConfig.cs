using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Config
{
    [CreateAssetMenu(fileName = "SpawnerConfig", menuName = "Config/SpawnerConfig")]
    public class SpawnerConfig : ScriptableObject
    {
        public List<GameObject> enemyCharacterPrefabs;

        public float SpawnRadius = 20f;
        public float SpawnCenterOffset = 2f;

        public int EnemiesPerWave = 5;
        public float TimeBetweenWaves = 10f;
        public int MaxTotalEnemies = 20;

        public LayerMask GroundLayerMask;
        public LayerMask ObstacleLayerMask;
        public float CheckSphereRadius = 0.5f;
    }
}