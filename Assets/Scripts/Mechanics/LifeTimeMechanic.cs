using Core;
using UnityEngine;

namespace Mechanics
{
    public sealed class LifeTimeMechanic
    {
        private float lifetime;
        private readonly AtomicEvent onDeath;

        public LifeTimeMechanic(AtomicVariable<float> lifeTime, AtomicEvent onDeath)
        {
            lifetime = lifeTime.Value;
            this.onDeath = onDeath;
        }

        public void Update()
        {
            lifetime -= Time.deltaTime;
            if (lifetime < 0)
            {
                onDeath?.Invoke();
            }
        }
    }
}