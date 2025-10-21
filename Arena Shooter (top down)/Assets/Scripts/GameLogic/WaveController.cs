using Assets.Scripts.Config;
using Assets.Scripts.Infrastructure.ObjectPool;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.GameLogic
{
    public class WaveController : MonoBehaviour, IWaveController
    {
        [SerializeField] private int _startWavePower = 20;
        [SerializeField] private float _wavePowerMultiplier = 1.2f;

        private IEnemySpawner _enemySpawner;

        private int _currentWavePower;
        private bool _isWaveActive = false;//TODO


        [Inject]
        private void Construct(IEnemySpawner enemySpawner, WaveConfig waveConfig)
        {
            _enemySpawner = enemySpawner;

            _startWavePower = waveConfig.startWavePower;
            _wavePowerMultiplier = waveConfig.wavePowerMultiplier;
        }

        /*calling by GameBootstrapper*/
        public void StartWaveController()
        {
            _isWaveActive = true;
            _currentWavePower = _startWavePower;

            StartNewWave();
        }


        private void StartNewWave()
        {
            RecalculateWavePower();

            _enemySpawner.SpawnWave(_currentWavePower);
        }

        private void RecalculateWavePower()
        {
            _currentWavePower += (int)(((float)_currentWavePower * _wavePowerMultiplier) - _currentWavePower);
        }
    }
}