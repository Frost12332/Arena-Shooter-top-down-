using UnityEngine;
using Zenject;

namespace Assets.Scripts.GameLogic
{
    public class CameraScript : MonoBehaviour, ICameraScript
    {
        [SerializeField] private GameObject _playerCharacter;

        [Inject]
        private void Construct(IPlayerController playerController)
        {
            _playerCharacter = playerController.GetPlayerCharacter();
        }

        public Camera GetCamera()
        {
            return gameObject.GetComponent<Camera>();
        }
    }
}