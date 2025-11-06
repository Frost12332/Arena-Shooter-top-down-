using Assets.Scripts.GameLogic.Health;
using UnityEngine;

namespace Assets.Scripts.GameLogic.Enemy.Group
{
    public class EnemyMember : MonoBehaviour
    {
        [SerializeField] private CharacterHealth _health;

        public CharacterHealth Health { get { return _health; } }
    }
}