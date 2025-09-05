using Assets.Scripts.GameLogic.Enemy;
using UnityEngine;

namespace Assets.Scripts.GameLogic.Player
{
    public class PlayerBehaviour : MonoBehaviour
    {
        [SerializeField] private BehaviourStrategy _spellAtackStrategy;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (_spellAtackStrategy.CanExecute())
                    _spellAtackStrategy.Execute();
            }
        }
    }
}