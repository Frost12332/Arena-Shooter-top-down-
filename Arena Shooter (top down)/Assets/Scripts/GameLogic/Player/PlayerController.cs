using Assets.Scripts.GameLogic.GameEventBus;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.GameLogic
{
    public class PlayerController : MonoBehaviour, IPlayerController
    {
        [SerializeField] private CharacterController _characterController;

        private IInputService _inputService;
        
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _rotationSpeed = 20f;

        private Vector3 _movement;

        [Inject]
        private void Construct(IInputService inputService)
        {
            _inputService = inputService;
            Debug.LogWarning("Character created");
        }

        void Update()
        {
            Vector2 _moveInput = _inputService.Movement();

            _movement.x = _moveInput.x;
            _movement.y = 0;
            _movement.z = _moveInput.y;

            _movement.Normalize();

            _characterController.Move(_movement * _moveSpeed * Time.deltaTime);

            RotateCharacter();
        }

        public GameObject GetPlayerCharacter()
        {
            return gameObject;
        }

        void RotateCharacter()
        {
            Vector3 direction = _inputService.Rotation();

            if (direction.sqrMagnitude > 0.001f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
            }
        }
    }
}