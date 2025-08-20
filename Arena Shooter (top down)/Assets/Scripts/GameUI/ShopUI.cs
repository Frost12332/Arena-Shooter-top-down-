using Assets.Scripts.Infrastructure.SceneLoad;
using UnityEngine;
using Zenject;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private const string _namePlayScene = "Game";
    [SerializeField] private const string _nameMenuScene = "Menu";

    private ISceneLoader _sceneLoader;



    [Inject]
    private void Construct(ISceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }

    public void PlayGame()
    {
        _sceneLoader.LoadScene(_namePlayScene, null);
    }

    public void MainMenu()
    {
        _sceneLoader.LoadScene(_nameMenuScene, null);
    }

    public void Settings()
    {
        Debug.LogError("Need implementation SettingsPannel");
    }
}
