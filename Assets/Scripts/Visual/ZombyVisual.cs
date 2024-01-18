using UnityEngine;
using Zomby;

namespace Visual
{
    public class ZombyVisual: MonoBehaviour
    {
        [SerializeField] private ZombyModel model;
        [SerializeField] private Animator animator;
        [SerializeField] public AnimatorDispatcher animatorDispatcher;
        private ZombyAnimatorController zombyAnimatorController;

        private void Awake()
        {
            zombyAnimatorController = new ZombyAnimatorController(model, animator, animatorDispatcher);
        }

        private void OnEnable()
        {
            zombyAnimatorController.OnEnable();
        }

        private void OnDisable()
        {
            zombyAnimatorController.OnDisable();
        }

        private void Update()
        {
            zombyAnimatorController.Update();
        }

    }
}
