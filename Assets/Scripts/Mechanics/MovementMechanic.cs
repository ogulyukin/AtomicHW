using Core;
using UnityEngine;

namespace Mechanics
{
    public sealed class MovementMechanic
    {
        private readonly AtomicVariable<bool> canMove;
        private readonly AtomicVariable<float> speed;
        private readonly AtomicVariable<Vector3> moveDirection;
        private readonly Transform targetTransform;

        public MovementMechanic(AtomicVariable<bool> canMove, AtomicVariable<float> speed, AtomicVariable<Vector3> moveDirection, Transform transform)
        {
            this.canMove = canMove;
            this.speed = speed;
            this.moveDirection = moveDirection;
            targetTransform = transform;
        }

        public void Update()
        {
            if(!canMove.Value) return;
            targetTransform.position += speed.Value * Time.deltaTime * moveDirection.Value;
        }
    }
}