using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameLogic.Enemy
{
    public class EnemyBrain : MonoBehaviour
    {
        [Header("Top strategy have highest priority")]
        [SerializeField] private List<BehaviourStrategy> _strategies;

        //float current = 0;
        //float max = 1;

        private void Update()
        {
            //current += Time.deltaTime;

            //if (current > max)
            //{
            //    Call3();
            //    current = 0;
            //}
            return;
            throw new NotImplementedException();

            foreach (BehaviourStrategy strategy in _strategies)
            {
                if (strategy != null && strategy.CanExecute())
                {
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
            _strategies[1].Execute();
        }

        [ContextMenu("Call3")]
        public void Call3()
        {
            _strategies[2].Execute();
        }
    }
}