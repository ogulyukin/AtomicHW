using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Zomby
{
    public sealed class ZombySpawnSystemConfig : MonoBehaviour
    {
        public GameObject zombyPrefab;
        public List<Transform> spawnPoints;
        public Transform parentTransform;
        public AtomicVariable<float> spawnTimeout;
        public Transform playerTransform;
    }
}
