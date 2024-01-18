using Core;
using UnityEngine;

namespace Mechanics
{
    public sealed class RotateForMouseMechanic
    {
        private Vector3 previousMousePosition;
        private readonly AtomicVariable<bool> isAlive;
        private readonly Transform targetTransform;
        private readonly Camera camera;

        public RotateForMouseMechanic(AtomicVariable<bool> isAlive, Transform targetTransform)
        {
            previousMousePosition = Input.mousePosition;
            this.isAlive = isAlive;
            this.targetTransform = targetTransform;
            camera = Camera.main;
        }

        public void Update()
        {
            if(!isAlive.Value)
                return;
            var mousePosition = Input.mousePosition;
            if (mousePosition != previousMousePosition)
            {
                if (Physics.Raycast(GetMouseRay(), out var hit))
                {
                    targetTransform.LookAt(new Vector3(hit.point.x, targetTransform.position.y, hit.point.z));
                    previousMousePosition = mousePosition;    
                }
            }
        }
        private Ray GetMouseRay()
        {
            return camera.ScreenPointToRay(Input.mousePosition);
        }
    }
}
