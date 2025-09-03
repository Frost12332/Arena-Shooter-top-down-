using Assets.Scripts.GameLogic.FireballBehaviour;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.GameLogic.Enemy
{
    public class SpellAtackStrategy : BehaviourStrategy
    {
        [SerializeField] private GameObject _spellAtackPrefab;
        [SerializeField] private Transform _spellAtackStartPosition;






        public override bool CanExecute()
        {
            return true;
        }

        protected override IEnumerator ExecuteBehavior()
        {
            GameObject gameObject = Instantiate(_spellAtackPrefab, _spellAtackStartPosition.position, Quaternion.identity, null);

            gameObject.GetComponent<Fireball>().Init(_spellAtackStartPosition.forward);


            yield return null;
        }
    }
}