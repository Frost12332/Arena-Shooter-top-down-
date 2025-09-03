using Assets.Scripts.Config;
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
        }

        private void BindSceneLoader()
        {
            Container.Bind<ICurtain>().To<Curtain>().FromComponentInNewPrefab(_curtainPrefab).AsSingle();
            Container.Bind<ISceneLoader>().To<SceneLoader>().FromComponentInNewPrefab(_sceneLoaderPrefab).AsSingle();
        }
    }
}