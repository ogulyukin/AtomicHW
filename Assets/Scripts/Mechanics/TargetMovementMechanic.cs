using Core;
using UnityEngine;

namespace Mechanics
{
    public sealed class TargetMovementMechanic
    {
        private readonly Transform targetTransform;
        private readonly AtomicVariable<Vector3> moveDirection;
        private readonly Transform entityTransform;
        private readonly AtomicVariable<bool> isAlive;


        public TargetMovementMechanic(Transform targetTransform, AtomicVariable<Vector3> moveDirection, Transform entityTransform, AtomicVariable<bool> isAlive)
        {
            this.targetTransform = targetTransform;
            this.moveDirection = moveDirection;
            this.entityTransform = entityTransform;
            this.isAlive = isAlive;
        }

        public void Update()
        {
            if (isAlive.Value)
            {
                entityTransform.LookAt(new Vector3(targetTransform.position.x, entityTransform.position.y, targetTransform.position.z));
                moveDirection.Value = entityTransform.forward;    
            }
        }
    }
}
