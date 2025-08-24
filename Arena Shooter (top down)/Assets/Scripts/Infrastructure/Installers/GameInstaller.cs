using Assets.Scripts.Config;
using Assets.Scripts.GameLogic;
using Assets.Scripts.GameUI;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Infrastructure.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _toolbarUIPrefab;
        [SerializeField] private GameObject _pauseMenuUIPrefab;

        [SerializeField] private GameConfig _gameConfig;

        public override void InstallBindings()
        {
            Container.Bind<IPauseMenuUI>().To<PauseMenuUI>().FromComponentInNewPrefab(_pauseMenuUIPrefab).AsSingle().NonLazy();
            Container.Bind<IToolbarUI>().To<ToolbarUI>().FromComponentInNewPrefab(_toolbarUIPrefab).AsSingle().NonLazy();

            Container.Bind<ICameraScript>().To<CameraScript>().FromComponentInNewPrefab(_gameConfig.gameCameraPrefab).AsSingle();
            Container.Bind<IPlayerController>().To<PlayerController>().FromComponentInNewPrefab(_gameConfig.playerCharacterPrefab).AsSingle();
        }
    }
}