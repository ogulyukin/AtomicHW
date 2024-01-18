using Core;
using UnityEngine;
using Zomby;

namespace Visual
{
    public class ZombyAnimatorController
    {
        private static readonly int MainState = Animator.StringToHash("State");
        private const int Idle = 0;
        private const int Move = 1;
        private const int Death = 5;
        private const int Attack = 3;
        private readonly AtomicVariable<Vector3> moveDirection;
        private readonly AtomicVariable<bool> isAttacking;
        private bool isAlive;
        private readonly AtomicVariable<bool> canMove;
        private readonly AtomicEvent onDeath;
        private readonly Animator animator;

        public ZombyAnimatorController(ZombyModel model, Animator animator, AnimatorDispatcher animatorDispatcher)
        {
            moveDirection = model.moveDirection;
            canMove = model.canMove;
            onDeath = model.onDeath;
            this.animator = animator;
            isAttacking = model.isAttacking;
            isAlive = true;
        }

        private void Die()
        {
            animator.SetInteger(MainState, Death);
            isAlive = false;
        }

        private int GetMainStateValue()
        {
            if (isAttacking.Value) return Attack;
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
