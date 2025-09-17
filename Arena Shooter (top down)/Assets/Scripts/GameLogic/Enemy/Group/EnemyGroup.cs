using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameLogic.Enemy.Group
{
    public class EnemyGroup : MonoBehaviour
    {
        [SerializeField] private EnemyMember _owner;

        [SerializeField] private int _maxGroupSize = 6;
        [SerializeField] private List<EnemyMember> _enemies;

        private void Start()
        {
            _enemies.Capacity = _maxGroupSize;
            Add(_owner);
        }

        public bool HasFreeMembership() =>
            _enemies.Count < _maxGroupSize;

        public int CountFreeMembership() =>
            _maxGroupSize - _enemies.Count;

        public void Add(EnemyMember enemy)
        {
            _enemies.Add(enemy);
        }

        public void Remove(EnemyMember enemy)
        {
            _enemies.Remove(enemy);
        }

        public IEnumerable<EnemyMember> Search(Predicate<EnemyMember> comparator)
        {
            foreach (EnemyMember enemy in _enemies)
            {
                if (comparator(enemy))
                {
                    yield return enemy;
                }
            }
        }

        /*if die owner kill all summon character*/
        /*Health.Die  ->  OnReleased  ->  EnemyGroup.OnDisable*/
        private void OnDisable()
        {
            List<EnemyMember> enemies = new List<EnemyMember>(_enemies);

            foreach(EnemyMember enemy in enemies)
            {
                if (enemy != _owner && enemy != null)
                    enemy.Health.Kill();
            }
        }
    }
}