using UnityEngine;

namespace Assets.Scripts.GameLogic.FireballBehaviour
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int _healthPoints;
        [SerializeField] private int _maxHealthPoints;

        public void TakeDamage(int damage)
        {
            _healthPoints = damage;
        }
    }
}