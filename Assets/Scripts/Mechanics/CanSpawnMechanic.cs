using Core;
using UnityEngine;

namespace Mechanics
{
    public sealed class CanSpawnMechanic
    {
        private readonly AtomicVariable<float> spawnTimeout;
        private readonly AtomicVariable<bool> canSpawn;
        private float currentTimeout;

        public CanSpawnMechanic(AtomicVariable<float> spawnTimeout, AtomicVariable<bool> canSpawn)
        {
            this.spawnTimeout = spawnTimeout;
            this.canSpawn = canSpawn;
        }

        public void Update()
        {
            currentTimeout -= Time.deltaTime;
            if (currentTimeout < 0)
            {
                currentTimeout = spawnTimeout.Value;
                canSpawn.Value = true;
            }
        }
    }
}
