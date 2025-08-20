namespace Assets.Scripts.Infrastructure.SceneLoad
{
    public interface ISceneLoader
    {
        void LoadScene(string sceneName, SceneHasBeenLoaded sceneHasBeenLoaded);
    }
}