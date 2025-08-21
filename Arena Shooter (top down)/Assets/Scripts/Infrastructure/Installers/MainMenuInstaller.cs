using UnityEngine;
using Zenject;

public class MainMenuInstaller : MonoInstaller
{
    [SerializeField] private GameObject _mainMenuUIPrefab;

    public override void InstallBindings()
    {
        Container.Bind<IMainMenuUI>().To<MainMenuUI>().FromComponentInNewPrefab(_mainMenuUIPrefab).AsSingle().NonLazy();
    }
}