using UnityEngine;

namespace Assets.Scripts.GameLogic
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _lifeTime = 3f;

        private void Start()
        {
            Destroy(gameObject, _lifeTime);
        }

        private void OnCollisionEnter(Collision collision)
        {
            // тут можно добавить урон монстрам
            Destroy(gameObject);
        }

    }
}