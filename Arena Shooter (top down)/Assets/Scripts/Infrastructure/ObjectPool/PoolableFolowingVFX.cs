using Assets.Scripts.GameLogic.Enemy.Group;
using System;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.ObjectPool
{
    public class PoolableFolowingVFX : Poolable, IReleasable
    {
        [SerializeField] private ParticleSystem[] _particleSystem;
        private Transform _target;

        public event Action OnReleased;

        private Action<GameObject> _activateHealingVFX;
        private Action _shutdownHealingVFX;
        private Action _releaseHealingVFX;


        public override void Activate(IPoolActivationData data)
        {
            if (data is  HealingData healingData)
            {
                _target = healingData.Target;
                _activateHealingVFX = healingData.ActivateHealingVFX;
                _shutdownHealingVFX = healingData.ShutdownHealingVFX;
                _releaseHealingVFX = healingData.ReleaseHealingVFX;

                _activateHealingVFX += ActivateVFX;
                _shutdownHealingVFX += ShutdonwVFX;
                _releaseHealingVFX += ReleaseVFX;

                gameObject.SetActive(true);
            }
        }
        protected override void Deactivate()
        {
            _target = null;

            _activateHealingVFX -= ActivateVFX;
            _shutdownHealingVFX -= ShutdonwVFX;
            _releaseHealingVFX -= ReleaseVFX;

            _activateHealingVFX = null;
            _shutdownHealingVFX = null;
            _releaseHealingVFX = null;

            gameObject.SetActive(false);
        }


        private void ActivateVFX(GameObject receivedGameObject)
        {
            if (this.gameObject == receivedGameObject)
            {
                gameObject.SetActive(true);
            }
        }
        private void ShutdonwVFX()
        {
            gameObject.SetActive(false);
        }

        private void ReleaseVFX()
        {
            OnReleased?.Invoke();
        }



        private void Update()
        {
            if (gameObject.activeSelf)
            {
                gameObject.transform.position = _target.transform.position;
            }

            for (int i = 0; i < _particleSystem.Length; i++)
            {
                if (_particleSystem[i].isStopped)
                    _particleSystem[i].Play();
            }
        }
    }
}