using Assets.Scripts.Config.Pool;
using UnityEngine;

namespace Assets.Scripts.Config
{
    [CreateAssetMenu(fileName = "WaveConfig", menuName = "Config/WaveConfig")]
    public class WaveConfig : ScriptableObject
    {
        public int startWavePower = 20;
        public float wavePowerMultiplier = 1.2f;
    }
}