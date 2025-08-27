using Assets.Scripts.GameLogic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Assets.Scripts.GameLogic.Enemy
{
    public class Aggro : MonoBehaviour
    {
        [SerializeField] private EnemyMove _enemyMove;
        
        private IPlayerController _playerCharacter;

        private void Start()
        {
            
        }
    }
}