using UnityEngine;

namespace Assets.Scripts.GameLogic.GameEventBus
{
    public class EventData { }

    public class CoinEventData : EventData
    {
        private int _coin;

        public int Coin { get { return _coin; } }

        public CoinEventData(int coin)
        {
            _coin = coin;
        }
    }

    public class PlayerEventData : EventData
    {
        private GameObject _player;

        public GameObject Player { get { return _player; } }

        public PlayerEventData(GameObject player)
        {
            _player = player;
        }
    }

    public class PlayerDieEventData : EventData { }

    public class EnemyDieEventData : EventData { }
}