using Core;
using UnityEngine;

namespace Mechanics
{
    public sealed class RotateMechanic
    {
        private const float RotateSpeed = 20f;
        private readonly AtomicVariable<bool> isAlive;
        private readonly AtomicVariable<Vector3> moveDirection;
        private readonly Transform targetTransform;

        public RotateMechanic(AtomicVariable<bool> isAlive, AtomicVariable<Vector3> moveDirection, Transform transform)
        {
            this.isAlive = isAlive;
            this.moveDirection = moveDirection;
            targetTransform = transform;
        }

        public void Update()
        {
            if(!isAlive.Value || moveDirection.Value == Vector3.zero) return;
            var rotation = Quaternion.LookRotation(moveDirection.Value);
            targetTransform.rotation = Quaternion.Lerp(targetTransform.rotation, rotation, Time.deltaTime * RotateSpeed);
        }
    }
}
