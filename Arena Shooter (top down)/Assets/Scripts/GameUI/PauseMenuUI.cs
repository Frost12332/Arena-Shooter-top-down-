using Assets.Scripts.Infrastructure.SceneLoad;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.GameUI
{
    public class PauseMenuUI : MonoBehaviour, IPauseMenuUI
    {
        [SerializeField] private GameObject _rootObject;
        [SerializeField] private const string _nameShopScene = "Shop";

        private ISceneLoader _sceneLoader;

        [Inject]
        private void Construct(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        private void Start()
        {
            HidePauseMenu();
        }

        public void Continue()
        {
            HidePauseMenu();
        }

        public void Settings()
        {
            Debug.LogError("Need implementation SettingsPannel");
        }

        public void EndBattle()
        {
            _sceneLoader.LoadScene(_nameShopScene, null);
        }

        public void ShowPauseMenu()
        {
            _rootObject.SetActive(true);
        }

        public void HidePauseMenu()
        {
            _rootObject.SetActive(false);
        }
    }
}