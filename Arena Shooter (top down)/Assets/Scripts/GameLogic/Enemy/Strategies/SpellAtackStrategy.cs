using Assets.Scripts.Config;
using Assets.Scripts.GameLogic.FireballBehaviour;
using Assets.Scripts.Infrastructure.ObjectPool;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.GameLogic.Enemy
{
    public class SpellAtackStrategy : BehaviourStrategy
    {
        [SerializeField] private PoolObjectTemplate _spellAtackTemplate;
        [SerializeField] private GameObject _spellAtackStartPosition;

        private IGameObjectPool _gameObjectPool;

        [Inject]
        private void Construct(IGameObjectPool gameObjectPool)
        {
            _gameObjectPool = gameObjectPool;
        }

        public override bool CanExecute()
        {
            return true;
        }

        protected override IEnumerator ExecuteBehavior()
        {
            Poolable spellAtack = _gameObjectPool.GetObject(_spellAtackTemplate.Id);
            ProjectileData projectileData = new ProjectileData(_spellAtackStartPosition.transform.position, _spellAtackStartPosition.transform.forward);

            spellAtack.Activate(projectileData);

            //spellAtack.Activate(_spellAtackStartPosition.position);

            //GameObject gameObject = Instantiate(_spellAtackPrefab, _spellAtackStartPosition.position, Quaternion.identity, null);

            //gameObject.GetComponent<Fireball>().Init(_spellAtackStartPosition.forward);


            yield return null;
        }
    }
}