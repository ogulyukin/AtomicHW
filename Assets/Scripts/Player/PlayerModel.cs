using System.Collections.Generic;
using Core;
using Mechanics;
using UnityEngine;

namespace Player
{
    public sealed class PlayerModel : MonoBehaviour, ICharacter
    {
        public AtomicVariable<int> health;
        public AtomicVariable<int> amoAmount;
        public AtomicVariable<int> maxAmoAmount;
        public AtomicVariable<bool> isAlive;
        public AtomicVariable<float> speed;
        public Transform firePoint;
        public Bullet.Bullet bullet;
        public AtomicVariable<float> shootTimeout;
        public AtomicVariable<int> addAmoTimeout;
        public AtomicVariable<Vector3> moveDirection;
        public AtomicVariable<bool> canMove;
        public AtomicVariable<bool> canShoot;
        public AtomicEvent<int> OnTakeDamage { get; } = new();
        public AtomicEvent onDeath;
        public AtomicEvent onFireRequested;
        public AtomicEvent onFire;
        public AtomicEvent onReloadAmo;
        
        private readonly List<IEventMechanic> mechanics = new();
        private MovementMechanic movementMechanic;
        private RotateForMouseMechanic rotateForMouseMechanic;
        private CanShootEventMechanic canShootEventMechanic;
        private AddAmoMechanic addAmoMechanic;
        private void Awake()
        {
            mechanics.Add(new PlayerCanMoveMechanic(canMove, isAlive));
            mechanics.Add(new TakeDamageEventMechanic(health, OnTakeDamage));
            mechanics.Add(new DeathEventMechanic(health,isAlive, onDeath));
            mechanics.Add( new ShootEventMechanic(onFireRequested, firePoint, bullet, transform, canShoot, onFire));
            movementMechanic = new MovementMechanic(canMove, speed, moveDirection, transform);
            rotateForMouseMechanic = new RotateForMouseMechanic(isAlive, transform);
            canShootEventMechanic = new CanShootEventMechanic(canShoot, amoAmount, shootTimeout, onFire, isAlive);
            mechanics.Add(canShootEventMechanic);
            addAmoMechanic = new AddAmoMechanic(addAmoTimeout, amoAmount, isAlive, onReloadAmo, maxAmoAmount);
            addAmoMechanic.Awake();
            isAlive.Value = true;
        }
        
        private void Update()
        {
            movementMechanic.Update();
            rotateForMouseMechanic.Update();
            canShootEventMechanic.Update();
        }

        private void OnEnable()
        {
            foreach (var obj in mechanics)
            {
                obj.OnEnable();
            }
        }

        private void OnDisable()
        {
            foreach (var obj in mechanics)
            {
                obj.OnDisable();
            }
        }
    }
}
