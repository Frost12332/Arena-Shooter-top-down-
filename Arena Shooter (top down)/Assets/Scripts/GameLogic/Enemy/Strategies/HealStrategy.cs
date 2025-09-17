using Assets.Scripts.Config;
using Assets.Scripts.GameLogic.Enemy.CustomAnimatorContol;
using Assets.Scripts.GameLogic.Enemy.Group;
using Assets.Scripts.Infrastructure.ObjectPool;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Searcher;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.GameLogic.Enemy
{
    public class HealStrategy : BehaviourStrategy
    {
        [SerializeField] private EnemyGroup _enemyGroup;
        [SerializeField] private PoolObjectTemplate _healVFX;
        [SerializeField] private EnemyMageAnimator _enemyMageAnimator;


        [SerializeField] private float _maxTimeForHealing = 5.0f;
        [SerializeField] private float _tickInterval = 0.25f;
        [SerializeField] private int _healAmount = 5;
        private float _currentHealTime;

        private bool _animationInterrupt;

        private IGameObjectPool _gameObjectPool;


        private event Action<GameObject> _activateHealingVFX;
        private event Action _shutdownHealingVFX;
        private event Action _releaseHealingVFX;



        [Inject]
        private void Construct(IGameObjectPool pool)
        {
            _gameObjectPool = pool;
        }


        private void Awake()
        {
            _activateHealingVFX = delegate { };
            _shutdownHealingVFX = delegate { };
            _releaseHealingVFX = delegate { };
        }


        public override bool CanExecute()
        {
            return true;
        }

        protected override IEnumerator ExecuteBehavior()
        {
            _enemyMageAnimator.StartPlayHeal();

            _animationInterrupt = false;
            _currentHealTime = 0;

            void OnAnimationInterupt()
            {
                _animationInterrupt = true;
            }

            _enemyMageAnimator.OnSpellCastHealEnd += OnAnimationInterupt;


            yield return Healing();


            _enemyMageAnimator.OnSpellCastHealEnd -= OnAnimationInterupt;

            _enemyMageAnimator.StopPlayeHeal();
        }

        private IEnumerator Healing()
        {
            IEnumerable<EnemyMember> healGroup = _enemyGroup.Search(IsNeedHeal);

            if (healGroup.Any())/*Group for heal empty, stop healing*/
                SpawnVFXForAllGroup(healGroup);
            else
                _animationInterrupt = true;

            while (!_animationInterrupt)
            {
                _shutdownHealingVFX?.Invoke();

                foreach (EnemyMember heal in healGroup)
                {
                    if (IsNeedHeal(heal))
                    {
                        _activateHealingVFX?.Invoke(heal.gameObject);
                        heal.Health.Healing(_healAmount);
                    }
                }

                yield return new WaitForSeconds(_tickInterval);
                _currentHealTime += _tickInterval;

                if (_currentHealTime >= _maxTimeForHealing)
                    _animationInterrupt = true;
            }

            _releaseHealingVFX?.Invoke();
        }


        private void SpawnVFXForAllGroup(IEnumerable<EnemyMember> healGroup)
        {
            foreach (EnemyMember enemy in healGroup)
            {
                Poolable poolable = _gameObjectPool.GetObject(_healVFX.Id);
                HealingData healingData = new HealingData(enemy.transform, _activateHealingVFX, _shutdownHealingVFX, _releaseHealingVFX);

                poolable.Activate(healingData);
            }
        }

        private bool IsNeedHeal(EnemyMember data)
        {
            return data.Health.IsNeedHealing();
        }
    }
}