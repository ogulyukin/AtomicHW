using System;
using System.Threading.Tasks;
using Core;

namespace Mechanics
{
    public sealed class AddAmoMechanic
    {
        private readonly AtomicVariable<int> addTimeout;
        private readonly AtomicVariable<int> amoAmount;
        private readonly AtomicVariable<bool> isAlive;
        private readonly AtomicVariable<int> maxAmoAmount;
        private readonly AtomicEvent reloadAmo;

        public AddAmoMechanic(AtomicVariable<int> addTimeout, AtomicVariable<int> amoAmount, AtomicVariable<bool> isAlive, AtomicEvent reloadAmo, AtomicVariable<int> maxAmoAmount)
        {
            this.addTimeout = addTimeout;
            this.amoAmount = amoAmount;
            this.isAlive = isAlive;
            this.reloadAmo = reloadAmo;
            this.maxAmoAmount = maxAmoAmount;
        }

        public async void Awake()
        {
            while (isAlive.Value)
            {
                await Task.Delay(TimeSpan.FromSeconds(addTimeout.Value));
                if (amoAmount.Value < maxAmoAmount.Value)
                {
                    amoAmount.Value++;
                    reloadAmo?.Invoke();    
                }
            }
        }
    }
}
