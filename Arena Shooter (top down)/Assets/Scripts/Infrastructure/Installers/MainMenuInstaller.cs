using Assets.Scripts.GameUI;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Infrastructure.Installers
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _mainMenuUIPrefab;

        public override void InstallBindings()
        {
            Container.Bind<IMainMenuUI>().To<MainMenuUI>().FromComponentInNewPrefab(_mainMenuUIPrefab).AsSingle().NonLazy();
        }
    }
}