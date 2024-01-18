using Core;
using UnityEngine;

namespace Mechanics
{
    public sealed class DealDamageMechanic : IEventMechanic
    {
        private readonly Transform targetTransform;
        private readonly AtomicVariable<int> damage;
        private readonly AtomicEvent onHit;

        public DealDamageMechanic(Transform targetTransform, AtomicVariable<int> damage, AtomicEvent onHit)
        {
            this.targetTransform = targetTransform;
            this.damage = damage;
            this.onHit = onHit;
        }


        public void OnEnable()
        {
            onHit.Subscribe(DealDamage);
        }

        public void OnDisable()
        {
            onHit.UnSubscribe(DealDamage);
        }

        private void DealDamage()
        {
            targetTransform.GetComponent<ICharacter>().OnTakeDamage.Invoke(damage.Value);
        }
    }
}
