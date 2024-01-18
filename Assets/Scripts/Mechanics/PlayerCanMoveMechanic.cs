using Core;

namespace Mechanics
{
    public sealed class PlayerCanMoveMechanic : IEventMechanic
    {
        private readonly AtomicVariable<bool> canMove;
        private readonly AtomicVariable<bool> isAlive;


        public PlayerCanMoveMechanic(AtomicVariable<bool> canMove, AtomicVariable<bool> isAlive)
        {
            this.canMove = canMove;
            this.isAlive = isAlive;
        }

        private void CanMove(bool value)
        {
            canMove.Value = value;
        }

        public void OnEnable()
        {
            isAlive.Subscribe(CanMove);
        }

        public void OnDisable()
        {
            isAlive.UnSubscribe(CanMove);
        }
    }
}
