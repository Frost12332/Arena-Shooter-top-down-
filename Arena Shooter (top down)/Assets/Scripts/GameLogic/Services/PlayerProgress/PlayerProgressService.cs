using Assets.Scripts.SaveData;
using System.Collections.Generic;

namespace Assets.Scripts.GameLogic.Services.PlayerProgress
{
    public class PlayerProgressService : IPlayerProgressService
    {
        public PlayerProgressData PlayerProgressData { get; set; }

        public List<IPlayerProgressUpdater> ProgressUpdaters { get; private set; } = new List<IPlayerProgressUpdater>();

        public void AddProgressUpdater(IPlayerProgressUpdater playerProgressUpdater) =>
            ProgressUpdaters.Add(playerProgressUpdater);

        public void RemoveProgressUpdater(IPlayerProgressUpdater playerProgressUpdater) =>
            ProgressUpdaters.Remove(playerProgressUpdater);

        public void AlertAllToUpdateData()
        {
            foreach (IPlayerProgressUpdater progressUpdater in ProgressUpdaters)
                progressUpdater.UpdateProgress(PlayerProgressData);
        }

        public void AlertAllToLoadData()
        {
            foreach (IPlayerProgressUpdater progressUpdater in ProgressUpdaters)
            {
                if (progressUpdater is IPlayerProgressLoader progressLoader)
                    progressLoader.LoadProgress(PlayerProgressData);
            }
        }
    }
}