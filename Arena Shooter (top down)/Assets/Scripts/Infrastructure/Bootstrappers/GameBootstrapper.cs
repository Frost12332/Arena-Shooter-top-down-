using Assets.Scripts.GameLogic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Infrastructure.Bootstrappers
{
    public class GameBootstrapper : MonoBehaviour
    {
        private IWaveController _waveController;

        [Inject]
        private void Construct(IWaveController waveController) =>
            _waveController = waveController;

        private void Start()
        {
            _waveController.StartWaveController();
        }
    }
}