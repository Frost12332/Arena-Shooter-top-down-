using Assets.Scripts.GameLogic.GameEventBus;
using Assets.Scripts.GameLogic.Services.PlayerProgress;
using Assets.Scripts.SaveData;
using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.GameUI.Game
{
    public class CoinTracker : MonoBehaviour, IPlayerProgressLoader
    {
        [SerializeField] private TextMeshProUGUI _coinText;

        private int _currentCoin = 0;

        private IPlayerProgressService _playerProgressService;
        private IEventBus _eventBus;

        [Inject]
        private void Construct(IPlayerProgressService playerProgressService, IEventBus eventBus)
        {
            _playerProgressService = playerProgressService;
            _eventBus = eventBus;
        }

        private void Awake()
        {
            _playerProgressService.AddProgressUpdater(this);
            _eventBus.Subscribe<EnemyDieEventData>(EnemyDie);
        }

        public void LoadProgress(PlayerProgressData playerProgressData) => 
            _currentCoin = _playerProgressService.PlayerProgressData.Coin;

        public void UpdateProgress(PlayerProgressData playerProgressData) => 
            _playerProgressService.PlayerProgressData.Coin = _currentCoin;


        private void EnemyDie(EnemyDieEventData eventData)/*TODO:need implementation*/
        {
            throw new NotImplementedException("need implementation");
        }


        private void OnDestroy()
        {
            _playerProgressService.RemoveProgressUpdater(this);
            _eventBus.Unsubscribe<EnemyDieEventData>(EnemyDie);
        }
    }
}