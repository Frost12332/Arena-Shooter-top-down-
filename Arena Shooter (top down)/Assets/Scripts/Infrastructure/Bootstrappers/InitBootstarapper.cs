using Assets.Scripts.Infrastructure.SceneLoad;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Bootstrappers
{
    public class InitBootstarapper : MonoBehaviour
    {
        [SerializeField] private const string _sceneName = "Menu";

        [SerializeField] private const float _delayTimeBeforeLoad = 3f;

        private ISceneLoader _sceneLoader;

        void Start()
        {
            _sceneLoader = FindFirstObjectByType<SceneLoader>();

            StartCoroutine(DelayLoad());
        }

        private IEnumerator DelayLoad()
        {
            yield return new WaitForSeconds(_delayTimeBeforeLoad);

            _sceneLoader.LoadScene(_sceneName, SceneLoaded);
        }

        private void SceneLoaded()
        {

        }
    }
}