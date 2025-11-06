using Assets.Scripts.Config;
using Assets.Scripts.GameLogic.GameEventBus;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.GameLogic
{
    /*setup for Wave takes from WaveConfig*/
    public class WaveController : MonoBehaviour, IWaveController
    {
        [SerializeField] private int _startWavePower;
        [SerializeField] private float _wavePowerMultiplier;
        
        private IEventBus _eventBus;
        private IEnemySpawner _enemySpawner;

        private int _currentWavePower;
        
        [Inject]
        private void Construct(IEnemySpawner enemySpawner, WaveConfig waveConfig, IEventBus eventBus)
        {
            _enemySpawner = enemySpawner;

            _startWavePower = waveConfig.startWavePower;
            _wavePowerMultiplier = waveConfig.wavePowerMultiplier;

            _eventBus = eventBus;
            _eventBus.Subscribe<PlayerDieEventData>(OnPlayerDie);

        }

        /*calling by GameBootstrapper*/
        public void StartWaveController()
        {
            _currentWavePower = _startWavePower;
            _enemySpawner.AllEnemyDie += WaveKilled;

            StartWave();
        }

        private void StartWave() => 
            WaveKilled();

        private void WaveKilled()/*TODO: add some pannel for inform player*/
        {
            _enemySpawner.SpawnWave(_currentWavePower);

            RecalculateWavePower();
        }

        private void RecalculateWavePower()
        {
            _currentWavePower += (int)(((float)_currentWavePower * _wavePowerMultiplier) - _currentWavePower);
        }

        private void OnPlayerDie(PlayerDieEventData eventData)
        {
            /*call stop wave controller*/

            /*first just load shop scene*/
            /*after full ready life cycle can update and clear all scene with next change scene*/
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<PlayerDieEventData>(OnPlayerDie);
            _enemySpawner.AllEnemyDie -= WaveKilled;
        }
    }
}