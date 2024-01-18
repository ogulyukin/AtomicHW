using System;
using Core;
using Mechanics;
using Zenject;

namespace Zomby
{
    public sealed class ZombySpawnSystem : ITickable, IDisposable
    {
        public readonly AtomicVariable<int> killsCount = new();
        private readonly AtomicVariable<bool> canSpawn = new();
        private readonly AtomicEvent<ZombyModel> newZombySpawned = new();

        private readonly CanSpawnMechanic canSpawnMechanic;
        private readonly SpawnObjectMechanic spawnObjectMechanic;
        private readonly ManageSpawnedObjectsMechanic manageSpawnedObjectsMechanic;

        public ZombySpawnSystem(ZombySpawnSystemConfig config)
        {
            canSpawnMechanic = new CanSpawnMechanic(config.spawnTimeout, canSpawn);
            spawnObjectMechanic =
                new SpawnObjectMechanic(canSpawn, config.zombyPrefab, config.parentTransform, config.spawnPoints, config.playerTransform, newZombySpawned);
            spawnObjectMechanic.OnEnable();
            manageSpawnedObjectsMechanic = new ManageSpawnedObjectsMechanic(newZombySpawned, killsCount);
            manageSpawnedObjectsMechanic.OnEnable();
        }

        public void Tick()
        {
            canSpawnMechanic.Update();
        }

        public void Dispose()
        {
            spawnObjectMechanic.OnDisable();
            manageSpawnedObjectsMechanic.OnDisable();
        }
    }
}
