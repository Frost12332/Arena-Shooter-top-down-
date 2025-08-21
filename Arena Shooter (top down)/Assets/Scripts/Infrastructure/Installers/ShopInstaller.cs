using UnityEngine;
using Zenject;

public class ShopInstaller : MonoInstaller
{
    [SerializeField] private GameObject _shopUIPrefab;

    public override void InstallBindings()
    {
        Container.Bind<IShopUI>().To<ShopUI>().FromComponentInNewPrefab(_shopUIPrefab).AsSingle().NonLazy();
    }
}