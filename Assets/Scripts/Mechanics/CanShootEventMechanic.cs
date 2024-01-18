using Core;
using UnityEngine;

namespace Mechanics
{
    public sealed class CanShootEventMechanic : IEventMechanic
    {
        private readonly AtomicVariable<bool> canShoot;
        private readonly AtomicVariable<int> amoAmount;
        private readonly AtomicVariable<float> shootTimeout;
        private readonly AtomicVariable<bool> isAlive;
        private readonly AtomicEvent onFire;
        private float timeout;
        
        public CanShootEventMechanic(AtomicVariable<bool> canShoot, AtomicVariable<int> amoAmount, AtomicVariable<float> shootTimeout, AtomicEvent onFire, AtomicVariable<bool> isAlive)
        {
            this.canShoot = canShoot;
            this.amoAmount = amoAmount;
            this.shootTimeout = shootTimeout;
            this.onFire = onFire;
            this.isAlive = isAlive;
        }

        public void Update()
        {
            timeout -= Time.deltaTime;
            if (!isAlive.Value || amoAmount.Value == 0)
            {
                canShoot.Value = false;
                return;
            }
            if (timeout < 0)
            {
                canShoot.Value = true;
                timeout = shootTimeout.Value;
            }
        }

        private void ResetTimeout()
        {
            timeout = shootTimeout.Value;
            amoAmount.Value--;
            canShoot.Value = false;
        }

        public void OnEnable()
        {
            onFire.Subscribe(ResetTimeout);
        }

        public void OnDisable()
        {
            onFire.UnSubscribe(ResetTimeout);
        }
    }
}