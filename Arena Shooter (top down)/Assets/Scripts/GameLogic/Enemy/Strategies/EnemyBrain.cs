using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameLogic.Enemy
{
    public class EnemyBrain : MonoBehaviour
    {
        [Header("Top strategy have highest priority")]
        [SerializeField] private List<BehaviourStrategy> _strategies;

        private void Update()
        {
            foreach (BehaviourStrategy strategy in _strategies)
            {
                if (strategy != null && strategy.CanExecute())
                {
                    strategy.Execute();
                    break;
                }
            }
        }
    }
}