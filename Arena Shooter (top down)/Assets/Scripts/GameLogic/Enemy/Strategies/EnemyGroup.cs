using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameLogic.Enemy
{
    public delegate bool ComparableFunction(object compareData, GameObject data);

    public class EnemyGroup : MonoBehaviour
    {
        [SerializeField] List<GameObject> _enemies;

        private void Start()
        {
            AddEnemy(gameObject);
        }

        public int Count { get { return _enemies.Count; } }

        public void AddEnemy(GameObject enemy)
        {
            _enemies.Add(enemy);
        }

        public void RemoveEnemy(GameObject enemy)
        {
            _enemies.Remove(enemy);
        }

        public IEnumerable<GameObject> Search(object compareData, ComparableFunction comparator)
        {
            foreach (GameObject enemy in _enemies)
            {
                if (comparator(compareData, enemy))
                {
                    yield return enemy;
                }
            }
        }
    }
}