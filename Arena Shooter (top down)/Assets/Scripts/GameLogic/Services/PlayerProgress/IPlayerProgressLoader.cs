using Assets.Scripts.SaveData;

namespace Assets.Scripts.GameLogic.Services.PlayerProgress
{
    /*Load data from PLayerProgressData*/
    public interface IPlayerProgressLoader : IPlayerProgressUpdater
    {
        public void LoadProgress(PlayerProgressData playerProgressData);
    }
}