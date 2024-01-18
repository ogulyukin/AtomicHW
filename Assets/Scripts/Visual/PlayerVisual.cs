using Player;
using UnityEngine;

namespace Visual
{
    public sealed class PlayerVisual : MonoBehaviour
    {
        [SerializeField] private PlayerModel playerModel;
        [SerializeField] private Animator animator;
        private PlayerAnimatorController playerAnimatorController;

        private void Awake()
        {
            playerAnimatorController = new PlayerAnimatorController(playerModel.moveDirection, playerModel.canMove, animator,
                playerModel.onDeath);
        }

        private void OnEnable()
        {
            playerAnimatorController.OnEnable();
        }

        private void OnDisable()
        {
            playerAnimatorController.OnDisable();
        }

        private void Update()
        {
            playerAnimatorController.Update();
        }
    }
}
