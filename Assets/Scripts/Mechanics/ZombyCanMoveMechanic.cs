using Core;

namespace Mechanics
{
    public sealed class ZombyCanMoveMechanic
    {
        private readonly AtomicVariable<bool> isAlive;
        private readonly AtomicVariable<bool> canMove;
        private readonly AtomicVariable<bool> isAttacking;
        private readonly AtomicVariable<bool> attackDistanceReached;

        public ZombyCanMoveMechanic(AtomicVariable<bool> isAlive, AtomicVariable<bool> canMove, AtomicVariable<bool> isAttacking, AtomicVariable<bool> attackDistanceReached)
        {
            this.isAlive = isAlive;
            this.canMove = canMove;
            this.attackDistanceReached = attackDistanceReached;
            this.isAttacking = isAttacking;
        }

        public void Update()
        {
            canMove.Value = isAlive.Value && !attackDistanceReached.Value && !isAttacking.Value;
        }
    }
}
