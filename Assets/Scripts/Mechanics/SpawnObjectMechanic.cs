using System.Collections.Generic;
using Core;
using UnityEngine;
using Zomby;

namespace Mechanics
{
    public sealed class SpawnObjectMechanic : IEventMechanic
    {
        private readonly AtomicVariable<bool> canSpawn;
        private readonly GameObject spawnPrefab;
        private readonly Transform parentTransform;
        private readonly List<Transform> spawnPoint;
        private readonly AtomicEvent<ZombyModel> spawnEvent;

        public SpawnObjectMechanic(AtomicVariable<bool> canSpawn, GameObject spawnPrefab, Transform parentTransform, List<Transform> spawnPoint, Transform playerTransform, AtomicEvent<ZombyModel> spawnEvent)
        {
            this.canSpawn = canSpawn;
            this.spawnPrefab = spawnPrefab;
            this.parentTransform = parentTransform;
            this.spawnPoint = spawnPoint;
            this.spawnEvent = spawnEvent;
        }

        public void OnEnable()
        {
            canSpawn.Subscribe(Spawn);
        }

        public void OnDisable()
        {
            canSpawn.UnSubscribe(Spawn);
        }

        private void Spawn(bool value)
        {
            if (!value) return;
            var instance = Object.Instantiate(spawnPrefab, GetSpawnPoint(), spawnPrefab.transform.rotation , parentTransform);
            spawnEvent.Invoke(instance.GetComponent<ZombyModel>());
            canSpawn.Value = false;
        }

        private Vector3 GetSpawnPoint()
        {
            return spawnPoint[Random.Range(0, spawnPoint.Count - 1)].position;
        }
    }
}
