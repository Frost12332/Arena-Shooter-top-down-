using Assets.Scripts.GameLogic.AnimatorLogic.AnimatorContracts;
using UnityEngine;

namespace Assets.Scripts.GameLogic
{
    public class AnimatePlayerMove : MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        
        private IMoveAnimator _moveAnimator;

        private void Awake()
        {
            _moveAnimator = GetComponent<IMoveAnimator>();
        }

        private void Update()
        {
            _moveAnimator.PlayMove(_characterController.velocity.magnitude);
        }
    }
}