using Core;

namespace Mechanics
{
    public sealed class CanAttackMechanic
    {
        private readonly AtomicVariable<bool> isAlive;
        private readonly AtomicVariable<bool> canAttack;
        private readonly AtomicVariable<bool> attackDistanceReached;


        public CanAttackMechanic(AtomicVariable<bool> isAlive, AtomicVariable<bool> canAttack, AtomicVariable<bool> attackDistanceReached)
        {
            this.isAlive = isAlive;
            this.canAttack = canAttack;
            this.attackDistanceReached = attackDistanceReached;
        }

        public void Update()
        {
            canAttack.Value = isAlive.Value && attackDistanceReached.Value;
        }
    }
}
