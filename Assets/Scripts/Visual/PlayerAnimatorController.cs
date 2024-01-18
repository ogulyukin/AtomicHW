using Core;
using UnityEngine;


namespace Visual
{
    public sealed class PlayerAnimatorController
    {
        private static readonly int MainState = Animator.StringToHash("MainState");
        private const int Idle = 0;
        private const int Move = 1;
        private const int Death = 2;
        private readonly AtomicVariable<Vector3> moveDirection;
        private bool isAlive;
        private readonly AtomicVariable<bool> canMove;
        private readonly AtomicEvent onDeath;
        private readonly Animator animator;

        public PlayerAnimatorController(AtomicVariable<Vector3> moveDirection, AtomicVariable<bool> canMove, Animator animator, AtomicEvent onDeath, AnimatorDispatcher animatorDispatcher)
        {
            this.moveDirection = moveDirection;
            this.canMove = canMove;
            this.onDeath = onDeath;
            this.animator = animator;
            isAlive = true;
        }

        private void Die()
        {
            animator.SetInteger(MainState, Death);
            isAlive = false;
        }

        private int GetMainStateValue()
        {
            if (moveDirection.Value == Vector3.zero || !canMove.Value)
                return Idle;
            return Move;
        }

        public void Update()
        {
            if(!isAlive) return;
            animator.SetInteger(MainState, GetMainStateValue());
        }

        public void OnEnable()
        {
            onDeath.Subscribe(Die);
        }

        public void OnDisable()
        {
            onDeath.UnSubscribe(Die);
        }
    }
}
