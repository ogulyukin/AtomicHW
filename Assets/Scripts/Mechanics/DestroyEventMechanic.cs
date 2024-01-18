using Core;
using UnityEngine;

namespace Mechanics
{
    public sealed class DestroyEventMechanic : IEventMechanic
    {
        private readonly AtomicEvent onDeath;
        private readonly AtomicVariable<bool> canMove;
        private readonly GameObject gameObject;

        public DestroyEventMechanic(AtomicEvent onDeath, AtomicVariable<bool> canMove, GameObject gameObject)
        {
            this.onDeath = onDeath;
            this.canMove = canMove;
            this.gameObject = gameObject;
        }

        private void OnDeath()
        {
            if (!canMove.Value)
            {
                return;
            }

            canMove.Value = false;
            Object.Destroy(gameObject);
        }

        public void OnEnable()
        {
            onDeath.Subscribe(OnDeath);
        }

        public void OnDisable()
        {
            onDeath.UnSubscribe(OnDeath);
        }
    }
}