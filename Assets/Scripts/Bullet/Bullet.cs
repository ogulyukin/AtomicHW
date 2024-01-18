using Core;
using Mechanics;
using UnityEngine;

namespace Bullet
{
    public sealed class Bullet : MonoBehaviour
    {
        public AtomicVariable<float> speed;
        public AtomicVariable<Vector3> moveDirection;
        public AtomicVariable<bool> canMove;
        public AtomicVariable<int> damage;
        public AtomicVariable<float> lifeTime;
        public AtomicEvent onDeath;

        private MovementMechanic movementMechanic;
        private BulletCollisionMechanic bulletCollisionMechanic;
        private LifeTimeMechanic lifeTimeMechanic;
        private DestroyEventMechanic destroyEventMechanic;

        private void Awake()
        {
            movementMechanic = new MovementMechanic(canMove, speed, moveDirection, transform);
            bulletCollisionMechanic = new BulletCollisionMechanic(damage, onDeath);
            lifeTimeMechanic = new LifeTimeMechanic(lifeTime, onDeath);
            destroyEventMechanic = new DestroyEventMechanic(onDeath, canMove, gameObject);
        }

        private void OnEnable()
        {
            destroyEventMechanic.OnEnable();
        }

        private void OnDisable()
        {
            destroyEventMechanic.OnDisable();
        }

        private void Update()
        {
            movementMechanic.Update();
            lifeTimeMechanic.Update();
        }

        private void OnTriggerEnter(Collider other)
        {
            bulletCollisionMechanic.OnTriggerEnter(other);
        }
    }
}
