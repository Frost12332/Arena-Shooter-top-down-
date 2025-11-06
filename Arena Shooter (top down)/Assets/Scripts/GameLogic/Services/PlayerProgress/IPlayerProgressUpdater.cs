using Assets.Scripts.SaveData;

namespace Assets.Scripts.GameLogic.Services.PlayerProgress
{
    /*Update data in PLayerProgressData*/
    public interface IPlayerProgressUpdater
    {
        public void UpdateProgress(PlayerProgressData playerProgressData);
    }
}