using Core;
using UnityEngine;

namespace Mechanics
{
    public sealed class AttackDistanceCheckMechanic
    {
        private readonly AtomicVariable<float> attackDistance;
        private readonly AtomicVariable<bool> isAlive;
        private readonly AtomicVariable<bool> attackDistanceReached;
        private readonly Transform playerTransform;
        private readonly Transform targetTransform;

        public AttackDistanceCheckMechanic(AtomicVariable<float> attackDistance, AtomicVariable<bool> isAlive, AtomicVariable<bool> attackDistanceReached, Transform playerTransform, Transform targetTransform)
        {
            this.attackDistance = attackDistance;
            this.isAlive = isAlive;
            this.attackDistanceReached = attackDistanceReached;
            this.playerTransform = playerTransform;
            this.targetTransform = targetTransform;
        }

        public void Update()
        {
            if (isAlive.Value && Vector3.Distance(playerTransform.position, targetTransform.position) < attackDistance.Value)
            {
                attackDistanceReached.Value = true;
                return;
            }

            attackDistanceReached.Value = false;
        }
    }
}
