using Core;
using UnityEngine;

namespace Mechanics
{
    public sealed class TakeDamageEventMechanic : IEventMechanic
    {
        private readonly AtomicVariable<int> health;
        private readonly AtomicEvent<int> onTakeDamage;

        public TakeDamageEventMechanic(AtomicVariable<int> health, AtomicEvent<int> onTakeDamage)
        {
            this.health = health;
            this.onTakeDamage = onTakeDamage;
        }

        private void TakeDamage(int damage)
        {
            health.Value = Mathf.Max(0, health.Value - damage);
        }

        public void OnEnable()
        {
            onTakeDamage.Subscribe(TakeDamage);
        }
        
        public void OnDisable()
        {
            onTakeDamage.UnSubscribe(TakeDamage);
        }
    }
}