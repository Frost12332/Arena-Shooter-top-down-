using Assets.Scripts.GameLogic.Enemy.Group;
using System;
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

    public class NPCActivationData : IPoolActivationData
    {
        private Vector3 _position;
        private EnemyGroup _enemyGroup;

        public Vector3 Position { get { return _position; } }
        public EnemyGroup EnemyGroup { get { return _enemyGroup; } }

        public NPCActivationData(Vector3 position, EnemyGroup enemyGroup)
        {
            _position = position;
            _enemyGroup = enemyGroup;
        }
    }

    public class HealingData : IPoolActivationData
    {
        private Transform _target;

        private Action<GameObject> _activateHealingVFX;
        private Action _shutdownHealingVFX;
        private Action _releaseHealingVFX;


        public Transform Target { get { return _target; } }
        public Action<GameObject> ActivateHealingVFX { get { return _activateHealingVFX; } }
        public Action ShutdownHealingVFX { get { return _shutdownHealingVFX; } }
        public Action ReleaseHealingVFX { get { return _releaseHealingVFX; } }

        public HealingData(Transform target, Action<GameObject> activateHealingVFX, Action shutdownHealingVFX, Action releaseHealingVFX)
        {
            _target = target;
            _activateHealingVFX = activateHealingVFX;
            _shutdownHealingVFX = shutdownHealingVFX;
            _releaseHealingVFX = releaseHealingVFX;
        }
    }
}