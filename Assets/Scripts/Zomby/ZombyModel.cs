using System.Collections.Generic;
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

        private readonly List<IEventMechanic> mechanics = new();
        private MovementMechanic movementMechanic;
        private TargetMovementMechanic targetMovementMechanic;
        private ZombyCanMoveMechanic zombyCanMoveMechanic;
        private AttackMechanic attackMechanic;
        private AttackDistanceCheckMechanic attackDistanceCheckMechanic;
        private CanAttackMechanic canAttackMechanic;
        private DecayInactiveObjectMechanic decayInactiveObjectMechanic;

        private void Awake()
        {
            playerTransform = GameObject.FindWithTag("Player").transform;
            var zombyTransform = transform;
            attackDistanceCheckMechanic = new AttackDistanceCheckMechanic(attackDistance, isAlive,
                attackDistanceReached, playerTransform, zombyTransform);
            canAttackMechanic = new CanAttackMechanic(isAlive, canAttack, attackDistanceReached);
            movementMechanic = new MovementMechanic(canMove, speed, moveDirection, zombyTransform);
            targetMovementMechanic = new TargetMovementMechanic(playerTransform, moveDirection, zombyTransform, isAlive);
            zombyCanMoveMechanic =
                new ZombyCanMoveMechanic(isAlive, canMove, isAttacking, attackDistanceReached);
            attackMechanic = new AttackMechanic(canAttack, isAttacking, attackTimeout, onAttackRequested, onHit);
            mechanics.Add(new TakeDamageEventMechanic(health, OnTakeDamage));
            mechanics.Add(new DeathEventMechanic(health, isAlive, onDeath));
            mechanics.Add(new DealDamageMechanic(playerTransform, damage, onHit));
            decayInactiveObjectMechanic = new DecayInactiveObjectMechanic(decayTimeout, deleteMe, onDeath);
            mechanics.Add(decayInactiveObjectMechanic);
            mechanics.Add(new DeleteObjectMechanic(deleteMe, transform));
            mechanics.Add(new ZombyKillCountEventMechanic(this));
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
            foreach (var mechanic in mechanics)
            {
                mechanic.OnEnable();
            }
        }

        private void OnDisable()
        {
            foreach (var mechanic in mechanics)
            {
                mechanic.OnDisable();
            }
        }
    }
}
