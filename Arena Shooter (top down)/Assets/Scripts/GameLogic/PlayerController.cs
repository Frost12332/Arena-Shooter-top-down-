
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private Camera _playerCamera;

    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 20f;

    private Vector3 _moveInput;

    void Start()
    {

    }

    void Update()
    {
        // 1. —читываем ввод
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        _moveInput.x = h;
        _moveInput.y = 0;
        _moveInput.z = v;

        _moveInput.Normalize();

        _characterController.Move(_moveInput * _moveSpeed * Time.deltaTime);

        RotateToMouse();
    }

    void RotateToMouse()
    {
        Ray ray = _playerCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f, LayerMask.GetMask("Ground"))) // важно исключить игрока!
        {
            Vector3 lookPoint = hitInfo.point;
            Vector3 direction = (lookPoint - transform.position);
            direction.y = 0;

            if (direction.sqrMagnitude > 0.001f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
            }
        }
    }
}