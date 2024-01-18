using Core;
using Mechanics;
using UnityEngine;

namespace Zomby
{
    public class ZombyModel : MonoBehaviour, ICharacter
    {
        public Transform playerTransform;
        public AtomicVariable<int> health;
        public AtomicVariable<int> damage;
        public AtomicVariable<bool> isAlive;
        public AtomicVariable<float> speed;
        public AtomicVariable<float> attackTimeout;
        public AtomicVariable<float> attackDistance;
        public AtomicVariable<bool> attackDistanceReached;
        public AtomicVariable<Vector3> moveDirection;
        public AtomicVariable<bool> canMove;
        public AtomicVariable<bool> canAttack;
        public AtomicVariable<bool> isAttacking;
        public AtomicVariable<float> decayTimeout;
        public AtomicEvent<int> OnTakeDamage { get; } = new();
        public AtomicEvent onDeath;
        public AtomicEvent<ZombyModel> updateKillCount;
        public AtomicEvent onAttackRequested;
        public AtomicEvent onHit;
        public AtomicEvent deleteMe;

        private MovementMechanic movementMechanic;
        private TargetMovementMechanic targetMovementMechanic;
        private ZombyCanMoveMechanic zombyCanMoveMechanic;
        private AttackMechanic attackMechanic;
        private AttackDistanceCheckMechanic attackDistanceCheckMechanic;
        private CanAttackMechanic canAttackMechanic;
        private TakeDamageEventMechanic takeDamageEventMechanic;
        private DeathEventMechanic deathEventMechanic;
        private DealDamageMechanic dealDamageMechanic;
        private DecayInactiveObjectMechanic decayInactiveObjectMechanic;
        private DeleteObjectMechanic deleteObjectMechanic;
        private ZombyKillCountEventMechanic zombyKillCountEventMechanic;

        private void Awake()
        {
            playerTransform = GameObject.FindWithTag("Player").transform;
            attackDistanceCheckMechanic = new AttackDistanceCheckMechanic(attackDistance, isAlive,
                attackDistanceReached, playerTransform, transform);
            canAttackMechanic = new CanAttackMechanic(isAlive, canAttack, attackDistanceReached);
            movementMechanic = new MovementMechanic(canMove, speed, moveDirection, transform);
            targetMovementMechanic = new TargetMovementMechanic(playerTransform, moveDirection, transform, isAlive);
            zombyCanMoveMechanic =
                new ZombyCanMoveMechanic(isAlive, canMove, isAttacking, attackDistanceReached);
            attackMechanic = new AttackMechanic(canAttack, isAttacking, attackTimeout, onAttackRequested, onHit);
            takeDamageEventMechanic = new TakeDamageEventMechanic(health, OnTakeDamage);
            deathEventMechanic = new DeathEventMechanic(health, isAlive, onDeath);
            dealDamageMechanic = new DealDamageMechanic(playerTransform, damage, onHit);
            decayInactiveObjectMechanic = new DecayInactiveObjectMechanic(decayTimeout, deleteMe, onDeath);
            deleteObjectMechanic = new DeleteObjectMechanic(deleteMe, transform);
            zombyKillCountEventMechanic = new ZombyKillCountEventMechanic(this);
            isAlive.Value = true;
        }

        private void Update()
        {
            attackDistanceCheckMechanic.Update();
            canAttackMechanic.Update();
            attackMechanic.Update();
            zombyCanMoveMechanic.Update();
            targetMovementMechanic.Update();
            movementMechanic.Update();
            decayInactiveObjectMechanic.Update();
        }

        private void OnEnable()
        {
            takeDamageEventMechanic.OnEnable();
            deathEventMechanic.OnEnable();
            dealDamageMechanic.OnEnable();
            decayInactiveObjectMechanic.OnEnable();
            deleteObjectMechanic.OnEnable();
            zombyKillCountEventMechanic.OnEnable();
        }

        private void OnDisable()
        {
            takeDamageEventMechanic.OnDisable();
            deathEventMechanic.OnDisable();
            dealDamageMechanic.OnDisable();
            decayInactiveObjectMechanic.OnDisable();
            deleteObjectMechanic.OnDisable();
            zombyKillCountEventMechanic.OnDisable();
        }
    }
}
