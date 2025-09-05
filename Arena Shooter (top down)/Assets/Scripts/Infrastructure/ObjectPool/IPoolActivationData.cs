using UnityEngine;

namespace Assets.Scripts.Infrastructure.ObjectPool
{
    public interface IPoolActivationData { }

    public class PositionData : IPoolActivationData
    {
        private Vector3 _position;

        public Vector3 Position { get => _position; }

        public PositionData(Vector3 position)
        {
            _position = position;
        }
    }

    public class ProjectileData : IPoolActivationData
    {
        private Vector3 _position;
        private Vector3 _direction;

        public Vector3 Position { get => _position; }
        public Vector3 Direction { get => _direction; }

        public ProjectileData(Vector3 position, Vector3 direction)
        {
            _position = position;
            _direction = direction;
        }
    }
}