using UnityEngine;

namespace Assets.Scripts.GameLogic.Services.PlayerInput
{
    public class StandaloneInputService : IInputService
    {
        private const string VerticalAxisName = "Vertical";
        private const string HorizontalAxisName = "Horizontal";

        private Camera _gameCamera;
        private Transform _playerTransform;

        public StandaloneInputService(ICameraScript cameraScript, IPlayerController playerController)
        {
            _gameCamera = cameraScript.GetCamera();
            _playerTransform = playerController.GetPlayerCharacter().transform;
        }

        public bool IsPress(KeyCode keyCode) =>
            Input.GetKeyDown(keyCode);


        public Vector2 Movement() =>
            new Vector2(Input.GetAxis(HorizontalAxisName), Input.GetAxis(VerticalAxisName));

        public Vector3 Rotation()
        {
            Vector3 direction = Vector3.zero;
            Ray ray = _gameCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f, LayerMask.GetMask("Ground")))
            {
                Vector3 lookPoint = hitInfo.point;
                direction = lookPoint - _playerTransform.transform.position;
                direction.y = 0;
            }

            return direction;
        }
    }
}