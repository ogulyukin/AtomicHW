using UnityEngine;
using Zomby;

namespace Visual
{
    public sealed class ZombyVisual: MonoBehaviour
    {
        [SerializeField] private ZombyModel model;
        [SerializeField] private Animator animator;
        private ZombyAnimatorController zombyAnimatorController;

        private void Awake()
        {
            zombyAnimatorController = new ZombyAnimatorController(model, animator);
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
