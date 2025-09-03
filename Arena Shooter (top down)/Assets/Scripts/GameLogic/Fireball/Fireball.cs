using UnityEngine;

namespace Assets.Scripts.GameLogic.FireballBehaviour
{
    public class Fireball : MonoBehaviour
    {
        public float speed = 10f;
        public float maxFlyingDistance = 50f;

        public int damage = 20;
        public GameObject hitVFX;

        public Rigidbody rigitbody;

        Vector3 dir;
        Vector3 startPosition;

        public void Init(Vector3 direction)
        {
            dir = direction;

            startPosition = transform.position;

            rigitbody.isKinematic = false;
            rigitbody.interpolation = RigidbodyInterpolation.Interpolate;
        }

        void FixedUpdate()
        {
            rigitbody.MovePosition(rigitbody.position + dir * speed * Time.fixedDeltaTime);

            if (Vector3.Distance(startPosition, transform.position) > maxFlyingDistance)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {



            //var health = other.GetComponent<Health>();
            //if (health != null)
            //    health.TakeDamage(damage);

            //if (hitVFX != null)
            //    Instantiate(hitVFX, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}