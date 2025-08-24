using UnityEngine;

namespace Assets.Scripts.Config
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Config/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        public GameObject gameCameraPrefab;

        public GameObject playerCharacterPrefab;
    }
}