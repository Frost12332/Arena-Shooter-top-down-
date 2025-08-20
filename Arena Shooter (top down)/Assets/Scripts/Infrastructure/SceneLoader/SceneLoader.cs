using Assets.Scripts.UI.Curtain;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Assets.Scripts.Infrastructure.SceneLoad
{
    public delegate void SceneHasBeenLoaded();

    public class SceneLoader : MonoBehaviour, ISceneLoader
    {
        private ICurtain _curtain;

        private const float _waitingSeconds = 0.5f;

        [Inject]
        private void Construct(ICurtain curtain)
        {
            _curtain = curtain;
        }

        public void LoadScene(string sceneName, SceneHasBeenLoaded sceneHasBeenLoaded)
        {
            _curtain.Show();

            StartCoroutine(LoadSceneAsync(sceneName, sceneHasBeenLoaded));
        }

        private IEnumerator LoadSceneAsync(string sceneName, SceneHasBeenLoaded sceneHasBeenLoaded)
        {
            yield return new WaitForSeconds(_waitingSeconds);

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);

            while (!asyncOperation.isDone)
                yield return null;

            _curtain.Hide();

            sceneHasBeenLoaded?.Invoke();

            yield return new WaitForSeconds(_waitingSeconds);
        }
    }
}