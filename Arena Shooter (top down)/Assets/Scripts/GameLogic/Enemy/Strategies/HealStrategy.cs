using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.GameLogic.Enemy
{
    public class HealStrategy : BehaviourStrategy
    {
        [SerializeField] private EnemyGroup _enemyGroup;
        [SerializeField] private GameObject _healVFX;

        public override bool CanExecute()
        {
            return true;
        }

        protected override IEnumerator ExecuteBehavior()
        {
            yield return null;

            foreach(GameObject enemy in _enemyGroup.Search(gameObject, CompareFunction))
            {
                Instantiate(_healVFX, enemy.transform.position, Quaternion.identity, null);
            }
        }

        private bool CompareFunction(object compareData, GameObject data)
        {
            GameObject gameObject = compareData as GameObject;

            return UnityEngine.Random.Range(0, 100) > 50 ? true : false;
        }
    }
}