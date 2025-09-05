using UnityEngine;
using Zenject;

namespace Assets.Scripts.Infrastructure.ObjectPool
{
    public abstract class Poolable : MonoBehaviour
    {
        private string _id;

        private IReleasable _releasable;
        private IGameObjectPool _gameObjectPool;

        [Inject]
        private void Construct(IGameObjectPool gameObjectPool)
        {
            _gameObjectPool = gameObjectPool;
        }

        private void Awake()
        {
            _releasable = GetComponent<IReleasable>();

            Deactivate();
        }

        private void OnEnable()
        {
            _releasable.OnReleased += ReleasableEvent;
        }


        public void Initialize(string id) => _id = id;

        private void ReleasableEvent()
        {
            Deactivate();

            _gameObjectPool.ReleaseObject(_id, this);
        }


        private void OnDisable()
        {
            _releasable.OnReleased -= ReleasableEvent;
        }

        public abstract void Activate(IPoolActivationData data);

        protected abstract void Deactivate();



        [ContextMenu("ReleaseObject")]
        public void Release()//TODO: remove me
        {
            ReleasableEvent();
        }
    }
}