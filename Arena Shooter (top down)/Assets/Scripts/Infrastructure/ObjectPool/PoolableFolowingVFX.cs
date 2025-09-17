using Assets.Scripts.GameLogic.Enemy;
using Assets.Scripts.GameLogic.Enemy.Group;
using System;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.ObjectPool
{
    public class PoolableFolowingVFX : Poolable, IReleasable
    {
        [SerializeField] private ParticleSystem[] _particleSystem;
        private Transform _target;
        private IBuffVFXController _buffVFXController;

        public event Action OnReleased;



        public override void Activate(IPoolActivationData data)
        {
            if (data is  BuffVFXData healingData)
            {
                _target = healingData.Target;
                _buffVFXController = healingData.BuffVFXController;


                _buffVFXController.ActivateBuffVFX += ActivateVFX;
                _buffVFXController.DeactivateBuffVFX += DeactivateVFX;
                _buffVFXController.ReleaseBuffVFX += ReleaseVFX;


                gameObject.SetActive(true);
            }
        }
        protected override void Deactivate()
        {
            _target = null;


            _buffVFXController.ActivateBuffVFX -= ActivateVFX;
            _buffVFXController.DeactivateBuffVFX -= DeactivateVFX;
            _buffVFXController.ReleaseBuffVFX -= ReleaseVFX;


            _buffVFXController = null;

            gameObject.SetActive(false);
        }

        private void ActivateVFX(GameObject receivedGameObject)
        {
            if (_target.gameObject == receivedGameObject)
            {
                if (!gameObject.activeSelf)
                    gameObject.SetActive(true);
            }
        }

        private void DeactivateVFX(GameObject receivedGameObject)
        {
            if (_target.gameObject == receivedGameObject)
            {
                if (gameObject.activeSelf) 
                    gameObject.SetActive(false);
            }
        }

        private void ReleaseVFX()
        {
            OnReleased?.Invoke();
        }

        private void Update()
        {
            if (gameObject.activeSelf)
                gameObject.transform.position = _target.transform.position;
        }
    }
}