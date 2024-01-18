using Core;
using UnityEngine;

namespace Mechanics
{
    public sealed class BulletCollisionMechanic
    {
        private readonly AtomicVariable<int> damage;
        private readonly AtomicEvent onDeath;

        public BulletCollisionMechanic(AtomicVariable<int> damage, AtomicEvent onDeath)
        {
            this.damage = damage;
            this.onDeath = onDeath;
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ICharacter character))
            {
                character.OnTakeDamage.Invoke(damage.Value);
            }
            onDeath?.Invoke();
        }
    }
}