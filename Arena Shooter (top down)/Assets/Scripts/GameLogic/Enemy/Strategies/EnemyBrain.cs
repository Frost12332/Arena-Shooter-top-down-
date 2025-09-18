using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameLogic.Enemy
{
    public class EnemyBrain : MonoBehaviour
    {
        [Header("Top strategy have highest priority")]
        [SerializeField] private List<BehaviourStrategy> _strategies;

        private BehaviourStrategy _currentStrategy = null;


        private void Update()
        {
            if (_currentStrategy != null && _currentStrategy.IsExecute())
                return;

            foreach (BehaviourStrategy strategy in _strategies)
            {
                if (strategy != null && strategy.CanExecute())
                {
                    _currentStrategy = strategy;
                    strategy.Execute();
                    break;
                }
            }
        }

        [ContextMenu("Call1")]
        public void Call1()
        {
            _strategies[0].Execute();
        }

        [ContextMenu("Call2")]
        public void Call2()
        {
            if (_strategies[1].CanExecute())
                _strategies[1].Execute();
        }

        [ContextMenu("Call3")]
        public void Call3()
        {
            _strategies[2].Execute();
        }
    }
}