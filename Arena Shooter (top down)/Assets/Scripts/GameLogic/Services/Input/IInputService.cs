using UnityEngine;

namespace Assets.Scripts.GameLogic
{
    public interface IInputService
    {
        public bool IsPress(KeyCode keyCode);

        public Vector2 Movement();

        public Vector3 Rotation();
    }
}