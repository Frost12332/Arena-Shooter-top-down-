using Assets.Scripts.GameLogic.Enemy.CustomAnimatorContol;
using Assets.Scripts.GameLogic.Enemy.Group;
using Assets.Scripts.Infrastructure.ObjectPool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Searcher;
using UnityEngine;

namespace Assets.Scripts.GameLogic.Enemy
{
    public class HealStrategy : BehaviourStrategy
    {
        [SerializeField] private EnemyGroup _enemyGroup;
        [SerializeField] private GameObject _healVFX;
        [SerializeField] private EnemyMageAnimator _enemyMageAnimator;






        public override bool CanExecute()
        {
            return true;
        }

        protected override IEnumerator ExecuteBehavior()
        {
            yield return null;

            //_enemyMageAnimator.StartPlayHeal();
            //bool animationInterupt = false;
            //void OnAnimationInterrupt()
            //{ animationInterupt = true; }
            //_enemyMageAnimator.OnHealEnd += OnAnimationInterrupt;
            
            //SpawnVFXForAllGroup();

            //while (currentTime < totalTimeForHealing)
            //{
            //    var healGroup = _enemyGroup.Search(HealthComparator);
            //    if (healGroup == false)
            //        break;
            //    ResetSignal?.Invoke();
            //    foreach ( var heal in healGroup)
            //    {
            //        ActivateSignal(heal);
            //    }

            //    yield return new WaitForSeconds(0.25f);
            //    currentTime += 0.25f;

            //    foreach (var heal in healGroup)
            //        heal.Health.Heal(5);
            //}
            //_enemyMageAnimator.OnHealEnd -= OnAnimationInterrupt;
        }

        private void SpawnVFXForAllGroup()
        {
            //foreach (EnemyMember enemy in healGroup)
            //{
            //    Poolable vfx = _gameObjectPool.GetObject(_healingVFXObjectTemplate.Id);
            //    HealingData healingData = ...;
            //    vfx.Activate(healingData);
            //}
        }

        private bool CompareFunction(EnemyMember data)
        {
            return data.Health.IsNeedHealing();
        }
    }
}