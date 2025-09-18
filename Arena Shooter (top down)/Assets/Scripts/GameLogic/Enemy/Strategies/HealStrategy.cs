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
    public class HealStrategy : BehaviourStrategy, IBuffVFXController
    {
        private const float second = 1.0f;

        [SerializeField] private EnemyGroup _enemyGroup;
        [SerializeField] private PoolObjectTemplate _healVFX;
        [SerializeField] private EnemyMageAnimator _enemyMageAnimator;


        [SerializeField] private float _maxTimeForHealing = 5.0f;
        [SerializeField] private float _tickInterval = 0.25f;
        [SerializeField] private int _healAmountInSeconds = 5;
        private float _currentHealTime;
        private float _healTickAcumulator;

        private bool _animationInterrupt;

        private IGameObjectPool _gameObjectPool;

        public event Action<GameObject> ActivateBuffVFX;
        public event Action<GameObject> DeactivateBuffVFX;
        public event Action ReleaseBuffVFX;

        [Inject]
        private void Construct(IGameObjectPool pool)
        {
            _gameObjectPool = pool;
        }

        public override bool CanExecute()
        {
            return _enemyGroup.Search(IsNeedHeal).Any();
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
            EnemyMember[] healGroup = _enemyGroup.Search(IsNeedHeal).ToArray();

            if (healGroup.Any())/*Group for heal empty, stop healing*/
                SpawnVFXForAllGroup(healGroup);
            else
                _animationInterrupt = true;

            while (!_animationInterrupt)
            {
                foreach (EnemyMember heal in healGroup)
                {
                    if (IsNeedHeal(heal))
                    {
                        ActivateBuffVFX?.Invoke(heal.gameObject);

                        if (_healTickAcumulator >= second)/*Healing animation play longer because WaitForSecond*/
                            heal.Health.Healing(_healAmountInSeconds);
                    }
                    else
                        DeactivateBuffVFX?.Invoke(heal.gameObject);
                }

                if (_healTickAcumulator > second)
                    _healTickAcumulator = 0;

                yield return new WaitForSeconds(_tickInterval);
                _currentHealTime += _tickInterval;
                _healTickAcumulator += _tickInterval;

                if (_currentHealTime >= _maxTimeForHealing || IsAllGroupHealed(healGroup))
                    _animationInterrupt = true;
            }

            ReleaseBuffVFX?.Invoke();
        }


        private void SpawnVFXForAllGroup(IEnumerable<EnemyMember> healGroup)
        {
            foreach (EnemyMember enemy in healGroup)
            {
                Poolable poolable = _gameObjectPool.GetObject(_healVFX.Id);
                BuffVFXData healingData = new BuffVFXData(enemy.transform, this);

                poolable.Activate(healingData);
            }
        }

        private bool IsNeedHeal(EnemyMember data)
        {
            return data.Health.IsNeedHealing();
        }

        private bool IsAllGroupHealed(EnemyMember[] healGroup)
        {
            foreach (EnemyMember enemy in healGroup)
            {
                if (IsNeedHeal(enemy))
                    return false;
            }
            return true;
        }
    }
}