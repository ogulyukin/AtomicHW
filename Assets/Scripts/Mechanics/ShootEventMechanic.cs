using Core;
using UnityEngine;

namespace Mechanics
{
    public sealed class ShootEventMechanic : IEventMechanic
    {
        private readonly AtomicEvent onFireRequested;
        private readonly Transform firePoint;
        private readonly Bullet.Bullet bullet;
        private readonly Transform transform;
        private readonly AtomicVariable<bool> canShoot;
        private readonly AtomicEvent onFire;

        public ShootEventMechanic(AtomicEvent onFireRequested, Transform firePoint, Bullet.Bullet bullet, Transform transform, AtomicVariable<bool> canShoot, AtomicEvent onFire)
        {
            this.onFireRequested = onFireRequested;
            this.firePoint = firePoint;
            this.bullet = bullet;
            this.transform = transform;
            this.canShoot = canShoot;
            this.onFire = onFire;
        }

        private void OnFireRequested()
        {
            if(!canShoot.Value)
                return;
            var bulletInstance = Object.Instantiate(bullet, firePoint.position, firePoint.rotation);
            bulletInstance.moveDirection.Value = transform.forward;
            onFire?.Invoke();
        }

        public void OnEnable()
        {
            onFireRequested.Subscribe(OnFireRequested);
        }

        public void OnDisable()
        {
            onFireRequested.UnSubscribe(OnFireRequested);
        }
    }
}