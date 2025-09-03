using UnityEngine;
using Zenject;

namespace Assets.Scripts.Infrastructure.ObjectPool
{
    public class Poolable : MonoBehaviour
    {
        private string _id;
        private IReleasable _releasable;
        private IGameObjectPool _gameObjectPool;

        [Inject]
        private void Construct(IGameObjectPool gameObjectPool)
        {
            _gameObjectPool = gameObjectPool;
        }

        private void Start()
        {
            _releasable = GetComponent<IReleasable>();
            _releasable.OnReleased += ReleasableEvent;
        }


        public void SetId(string id) => _id = id;

        private void ReleasableEvent()
        {
            gameObject.SetActive(false);

            _gameObjectPool.ReleaseObject(_id, gameObject);
        }


        private void OnDestroy()
        {
            _releasable.OnReleased -= ReleasableEvent;
        }



        [ContextMenu("ReleaseObject")]
        public void Release()//TODO: remove me
        {
            ReleasableEvent();
        }
    }
}