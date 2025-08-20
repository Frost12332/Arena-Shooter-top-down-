using Assets.Scripts.Infrastructure.SceneLoad;
using UnityEditor;
using UnityEngine;
using Zenject;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private const string _nameShopScene = "Shop";

    private ISceneLoader _sceneLoader;

    [Inject]
    private void Construct(ISceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }

    public void StartGame()
    {
        _sceneLoader.LoadScene(_nameShopScene, null);
    }

    public void Settings()
    {
        Debug.LogError("Need implementation SettingsPannel");
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
