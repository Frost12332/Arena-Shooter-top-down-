using Assets.Scripts.Infrastructure.SceneLoad;
using UnityEditor;
using UnityEngine;
using Zenject;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private const string _nameNextScene = "Shop";

    private ISceneLoader _sceneLoader;

    [Inject]
    private void Construct(ISceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }

    public void StartGame()
    {
        _sceneLoader.LoadScene(_nameNextScene, null);
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
