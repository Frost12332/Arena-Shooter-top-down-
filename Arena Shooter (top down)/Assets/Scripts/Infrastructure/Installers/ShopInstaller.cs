using Assets.Scripts.GameUI;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Infrastructure.Installers
{
    public class ShopInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _shopUIPrefab;

        public override void InstallBindings()
        {
            Container.Bind<IShopUI>().To<ShopUI>().FromComponentInNewPrefab(_shopUIPrefab).AsSingle().NonLazy();
        }
    }
}