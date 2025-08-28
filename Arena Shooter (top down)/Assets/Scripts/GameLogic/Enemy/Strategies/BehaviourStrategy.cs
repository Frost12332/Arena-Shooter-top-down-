using System.Collections;
using UnityEngine;

namespace Assets.Scripts.GameLogic.Enemy
{
    public abstract class BehaviourStrategy : MonoBehaviour
    {
        private Coroutine _coroutine;
        private bool _isExecute = false;

        public abstract bool CanExecute();

        public bool IsExecute() => _isExecute;



        public void Execute()
        {
            if (_isExecute || _coroutine != null)
                return;

            _coroutine = StartCoroutine(Run());
        }

        private IEnumerator Run()
        {
            _isExecute = true;

            yield return ExecuteBehavior();

            _isExecute = false;
            _coroutine = null;
        }



        public void Interupt()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
            _isExecute = false;
            OnInterrupted();
        }

       

        protected abstract IEnumerator ExecuteBehavior();

        protected virtual void OnInterrupted() { }
    }
}