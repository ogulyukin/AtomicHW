using System.Collections.Generic;
using Core;
using Zomby;

namespace Mechanics
{
    public sealed class ManageSpawnedObjectsMechanic : IEventMechanic
    {
        private readonly AtomicEvent<ZombyModel> zombySpawnEvent;
        private readonly AtomicVariable<int> killCount;

        public ManageSpawnedObjectsMechanic(AtomicEvent<ZombyModel> zombySpawnEvent, AtomicVariable<int> killCount)
        {
            this.zombySpawnEvent = zombySpawnEvent;
            this.killCount = killCount;
        }

        private void AddZomby(ZombyModel zombyModel)
        {
            zombyModel.updateKillCount.Subscribe(OnZombyDeath);
        }

        private void OnZombyDeath(ZombyModel zombyModel)
        {
            zombyModel.updateKillCount.UnSubscribe(AddZomby);
            killCount.Value++;
        }

        public void OnEnable()
        {
            zombySpawnEvent.Subscribe(AddZomby);
        }

        public void OnDisable()
        {
            zombySpawnEvent.UnSubscribe(AddZomby);
        }
    }
}
