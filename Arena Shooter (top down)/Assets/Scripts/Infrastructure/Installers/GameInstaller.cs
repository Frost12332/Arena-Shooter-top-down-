using Assets.Scripts.Config;
using Assets.Scripts.GameLogic;
using Assets.Scripts.GameUI;
using Assets.Scripts.Infrastructure.Factory;
using Assets.Scripts.Infrastructure.ObjectPool;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Infrastructure.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _toolbarUIPrefab;
        [SerializeField] private GameObject _pauseMenuUIPrefab;

        [SerializeField] private GameObject _waveControllerPrefab;

        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private SpawnerConfig _spawnerConfig;


        [SerializeField] private PoolObjectCollection _poolObjectCollection;





        public override void InstallBindings()
        {
            NewMethod();



            Container.Bind<IGameFactory>().To<GameFactory>().FromNew().AsSingle();
            Container.Bind<IGameObjectPool>().To<GameObjectPool>().FromNew().AsSingle();

            Container.Bind<PoolObjectCollection>().FromInstance(_poolObjectCollection).AsSingle();

        }

        private void NewMethod()
        {
            Container.Bind<IPauseMenuUI>().To<PauseMenuUI>().FromComponentInNewPrefab(_pauseMenuUIPrefab).AsSingle().NonLazy();
            Container.Bind<IToolbarUI>().To<ToolbarUI>().FromComponentInNewPrefab(_toolbarUIPrefab).AsSingle().NonLazy();

            Container.Bind<ICameraScript>().To<CameraScript>().FromComponentInNewPrefab(_gameConfig.gameCameraPrefab).AsSingle();
            Container.Bind<IPlayerController>().To<PlayerController>().FromComponentInNewPrefab(_gameConfig.playerCharacterPrefab).AsSingle();

            Container.Bind<SpawnerConfig>().FromInstance(_spawnerConfig).AsSingle();

            Container.Bind<IEnemySpawner>().To<EnemySpawner>().AsSingle();

            Container.Bind<WaveController>().FromComponentInNewPrefab(_waveControllerPrefab).AsSingle().NonLazy();
        }
    }
}