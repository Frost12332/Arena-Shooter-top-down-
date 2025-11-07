using Assets.Scripts.GameLogic.Services.PlayerProgress;
using Assets.Scripts.SaveData;
using System;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.GameUI.Game
{
    public class ManaTracker : MonoBehaviour, IPlayerProgressLoader
    {
        [SerializeField] private TextMeshProUGUI _manaText;

        public void LoadProgress(PlayerProgressData playerProgressData)
        {
            throw new NotImplementedException();
        }

        public void UpdateProgress(PlayerProgressData playerProgressData)
        {
            throw new NotImplementedException();
        }
    }
}