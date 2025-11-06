using Assets.Scripts.SaveData;
using System.Collections.Generic;

namespace Assets.Scripts.GameLogic.Services.PlayerProgress
{
    public interface IPlayerProgressService
    {
        PlayerProgressData PlayerProgressData { get; set; }
        List<IPlayerProgressUpdater> ProgressUpdaters { get; }

        void AlertAllToLoadData();
        void AlertAllToUpdateData();

        void AddProgressUpdater(IPlayerProgressUpdater playerProgressUpdater);
        void RemoveProgressUpdater(IPlayerProgressUpdater playerProgressUpdater);
    }
}