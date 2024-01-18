using Core;
using Player;

namespace Mechanics
{
    public sealed class DeathEventMechanic : IEventMechanic
    {
        private readonly AtomicVariable<int> health;
        private readonly AtomicVariable<bool> isAlive;
        private readonly AtomicEvent onDeath;

        public DeathEventMechanic(AtomicVariable<int> health, AtomicVariable<bool> isAlive, AtomicEvent onDeath)
        {
            this.health = health;
            this.isAlive = isAlive;
            this.onDeath = onDeath;
        }

        public void OnEnable()
        {
            health.Subscribe(Die); 
        }

        public void OnDisable()
        {
            health.UnSubscribe(Die); 
        }

        private void Die(int value)
        {
            if (value == 0 && isAlive.Value)
            {
                isAlive.Value = false;
                onDeath?.Invoke();
            }
        }
    }
}