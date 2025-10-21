using Assets.Scripts.GameLogic.GameEventBus;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.GameLogic
{
    /*PlayerCharacter create by object pool not GameInstaller and during life time can be activate and deactivate*/
    public class CameraScript : MonoBehaviour, ICameraScript
    {
        [SerializeField] private GameObject _playerCharacter = null;

        Vector3 _offset = Vector3.zero;

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