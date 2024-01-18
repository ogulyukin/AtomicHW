using Core;
using UnityEngine;

namespace Mechanics
{
    public sealed class AttackMechanic
    {
        private readonly AtomicVariable<bool> canAttack;
        private readonly AtomicVariable<bool> isAttacking;
        private readonly AtomicVariable<float> attackTimeout;
        private readonly AtomicEvent onAttackRequest;
        private readonly AtomicEvent onHit;
        private float currentTimeout;


        public AttackMechanic(AtomicVariable<bool> canAttack, AtomicVariable<bool> isAttacking, AtomicVariable<float> attackTimeout, AtomicEvent onAttackRequest, AtomicEvent onHit)
        {
            this.canAttack = canAttack;
            this.isAttacking = isAttacking;
            this.attackTimeout = attackTimeout;
            this.onAttackRequest = onAttackRequest;
            this.onHit = onHit;
        }

        public void Update()
        {
            if (canAttack.Value && !isAttacking.Value)
            {
                isAttacking.Value = true;
                currentTimeout = attackTimeout.Value;
                onAttackRequest.Invoke();
                return;
            }
            
            if (isAttacking.Value)
            {
                currentTimeout -= Time.deltaTime;
                if (currentTimeout < 0)
                {
                    TryHit();
                }
            }
        }

        private void TryHit()
        {
            if (canAttack.Value)
            {
                onHit.Invoke();
            }

            isAttacking.Value = false;
        }
    }
}
