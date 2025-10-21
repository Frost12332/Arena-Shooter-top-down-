using Assets.Scripts.Config;
using Assets.Scripts.Config.Pool;
using Assets.Scripts.GameLogic;
using Assets.Scripts.GameLogic.GameEventBus;
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
        [SerializeField] private WaveConfig _waveConfig;


        [SerializeField] private PoolObjectCollection _poolObjectCollection;
        [SerializeField] private EnemyPoolObjectCollection _enemyPoolObjectCollection;
        [SerializeField] private PhantomPoolObjectCollection _phantomPoolObjectCollection;





        public override void InstallBindings()
        {
            NewMethod();



            Container.Bind<IGameFactory>().To<GameFactory>().FromNew().AsSingle();
            Container.Bind<IGameObjectPool>().To<GameObjectPool>().FromNew().AsSingle();

            Container.Bind<PoolObjectCollection>().FromInstance(_poolObjectCollection).AsSingle();
            Container.Bind<EnemyPoolObjectCollection>().FromInstance(_enemyPoolObjectCollection).AsSingle();
            Container.Bind<PhantomPoolObjectCollection>().FromInstance(_phantomPoolObjectCollection).AsSingle();



            Container.Bind<IInputService>().To<StandaloneInputService>().FromNew().AsSingle();
            Container.Bind<IEventBus>().To<EventBus>().FromNew().AsSingle();
        }

        private void NewMethod()
        {
            Container.Bind<IPauseMenuUI>().To<PauseMenuUI>().FromComponentInNewPrefab(_pauseMenuUIPrefab).AsSingle().NonLazy();
            Container.Bind<IToolbarUI>().To<ToolbarUI>().FromComponentInNewPrefab(_toolbarUIPrefab).AsSingle().NonLazy();

            Container.Bind<ICameraScript>().To<CameraScript>().FromComponentInNewPrefab(_gameConfig.gameCameraPrefab).AsSingle();
            Container.Bind<IPlayerController>().To<PlayerController>().FromComponentInNewPrefab(_gameConfig.playerCharacterPrefab).AsSingle();

            Container.Bind<SpawnerConfig>().FromInstance(_spawnerConfig).AsSingle();
            Container.Bind<WaveConfig>().FromInstance(_waveConfig).AsSingle();

            Container.Bind<IEnemySpawner>().To<EnemySpawner>().AsSingle();

            Container.Bind<IWaveController>().To<WaveController>().FromComponentInNewPrefab(_waveControllerPrefab).AsSingle().NonLazy();
        }
    }
}