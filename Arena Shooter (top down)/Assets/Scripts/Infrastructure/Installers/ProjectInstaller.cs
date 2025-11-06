using Assets.Scripts.Config;
using Assets.Scripts.GameLogic.Services.PlayerProgress;
using Assets.Scripts.Infrastructure.SceneLoad;
using Assets.Scripts.UI.Curtain;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Infrastructure.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _sceneLoaderPrefab;
        [SerializeField] private GameObject _curtainPrefab;

        


        public override void InstallBindings()
        {
            BindSceneLoader();
            BindPlayerProgressService();
        }

        private void BindSceneLoader()
        {
            Container.Bind<ICurtain>().To<Curtain>().FromComponentInNewPrefab(_curtainPrefab).AsSingle();
            Container.Bind<ISceneLoader>().To<SceneLoader>().FromComponentInNewPrefab(_sceneLoaderPrefab).AsSingle();
        }

        private void BindPlayerProgressService()
        {
            Container.Bind<IPlayerProgressService>().To<PlayerProgressService>().FromNew().AsSingle();
        }
    }
}