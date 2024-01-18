using Core;
using UnityEngine;

namespace Mechanics
{
    public sealed class DeleteObjectMechanic : IEventMechanic
    {
        private readonly AtomicEvent deleteMe;
        private readonly Transform targetTransform;

        public DeleteObjectMechanic(AtomicEvent deleteMe, Transform targetTransform)
        {
            this.deleteMe = deleteMe;
            this.targetTransform = targetTransform;
        }

        private void DestroyObject()
        {
            Object.Destroy(targetTransform.gameObject);
        }

        public void OnEnable()
        {
            deleteMe.Subscribe(DestroyObject);
        }

        public void OnDisable()
        {
            deleteMe.UnSubscribe(DestroyObject);
        }
    }
}
