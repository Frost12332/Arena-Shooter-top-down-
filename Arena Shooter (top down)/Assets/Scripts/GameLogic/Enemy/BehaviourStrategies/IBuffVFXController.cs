using System;
using UnityEngine;

namespace Assets.Scripts.GameLogic.Enemy.BehaviourStrategies
{
    public interface IBuffVFXController
    {
        public event Action<GameObject> ActivateBuffVFX;
        public event Action<GameObject> DeactivateBuffVFX;
        public event Action ReleaseBuffVFX;
    }
}