using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private GameObject _toolbarUIPrefab;
    [SerializeField] private GameObject _pauseMenuUIPrefab;

    public override void InstallBindings()
    {
        Container.Bind<IPauseMenuUI>().To<PauseMenuUI>().FromComponentInNewPrefab(_pauseMenuUIPrefab).AsSingle().NonLazy();
        Container.Bind<IToolbarUI>().To<ToolbarUI>().FromComponentInNewPrefab(_toolbarUIPrefab).AsSingle().NonLazy();
    }
}
