using System.Collections;
using UnityEngine;

namespace Assets.Scripts.GameLogic.GameResource
{
    public class ResourceGenerator : MonoBehaviour
    {
        [SerializeField] private ResourceType _resourceType;

        [SerializeField] private ResourceStorage _storage;

        [SerializeField] private float _timeForGenerateMana = 2.5f;
        [SerializeField] private float _countGeneratedMana = 5;

        private Coroutine _coroutine = null;
        private bool _canStartCoroutine;

        private void Awake()
        {
            if (_storage.GetResourceType() != _resourceType)
                _canStartCoroutine = false;
            else 
                _canStartCoroutine = true;
        }

        private void OnEnable()
        {
            if (_canStartCoroutine)
                _coroutine = StartCoroutine(GenerateMana());
        }

        private IEnumerator GenerateMana()
        {
            while (true)
            {
                yield return new WaitForSeconds(_timeForGenerateMana);

                _storage.Add(_countGeneratedMana);
            }
        }

        private void OnDisable()
        {
            if (_canStartCoroutine)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
        }
    }
}