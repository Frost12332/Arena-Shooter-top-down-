using Assets.Scripts.GameLogic.FireballBehaviour;
using UnityEngine;

namespace Assets.Scripts.GameLogic.Enemy.Group
{
    public class EnemyMember : MonoBehaviour
    {
        [SerializeField] private Health _health;

        public Health Health { get { return _health; } }
    }
}