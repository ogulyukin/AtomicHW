using Core;
using UnityEngine;

namespace Mechanics
{
    public class DecayInactiveObjectMechanic : IEventMechanic
    {
        private readonly AtomicVariable<float> deleteTimeout;
        private readonly AtomicEvent deleteMe;
        private readonly AtomicEvent onDeath;
        private bool isInactive;
        private float currentTimeout;

        public DecayInactiveObjectMechanic(AtomicVariable<float> deleteTimeout, AtomicEvent deleteMe, AtomicEvent onDeath)
        {
            this.deleteTimeout = deleteTimeout;
            this.deleteMe = deleteMe;
            this.onDeath = onDeath;
        }

        private void BeginDecay()
        {
            isInactive = true;
            currentTimeout = deleteTimeout.Value;
        }

        public void Update()
        {
            if(!isInactive) return;
            currentTimeout -= Time.deltaTime;
            if (currentTimeout < 0)
            {
                deleteMe.Invoke();
            }
        }

        public void OnEnable()
        {
            onDeath.Subscribe(BeginDecay);
        }

        public void OnDisable()
        {
            onDeath.UnSubscribe(BeginDecay);
        }
    }
}
