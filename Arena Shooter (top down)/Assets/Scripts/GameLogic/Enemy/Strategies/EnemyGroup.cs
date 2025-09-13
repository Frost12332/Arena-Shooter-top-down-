using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameLogic.Enemy
{
    public delegate bool ComparableFunction(object compareData, GameObject data);

    public class EnemyGroup : MonoBehaviour
    {
        [SerializeField] private int _maxGroupSize = 6;
        
        [SerializeField] private List<GameObject> _enemies;

        private void Start()
        {
            _enemies.Capacity = _maxGroupSize;

            AddEnemy(gameObject);
        }

        public bool HasFreeMembership() =>
            _enemies.Count < _maxGroupSize;

        public int CountFreeMembership() =>
            _maxGroupSize - _enemies.Count;

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