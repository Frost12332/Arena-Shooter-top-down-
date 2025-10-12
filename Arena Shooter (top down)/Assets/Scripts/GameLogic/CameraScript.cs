using UnityEngine;
using Zenject;

namespace Assets.Scripts.GameLogic
{
    public class CameraScript : MonoBehaviour, ICameraScript
    {
        [SerializeField] private GameObject _playerCharacter;

        Vector3 _offset;

        [Inject]
        private void Construct(IPlayerController playerController)
        {
            _playerCharacter = playerController.GetPlayerCharacter();
        }

        private void Start()
        {
            _offset = transform.position - _playerCharacter.transform.position;
        }

        public Camera GetCamera()
        {
            return gameObject.GetComponent<Camera>();
        }

        private void Update()
        {
            transform.position = Vector3.Lerp(transform.position, _playerCharacter.transform.position + _offset, Time.deltaTime);
        }
    }
}