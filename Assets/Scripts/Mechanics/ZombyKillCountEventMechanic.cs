using Zomby;

namespace Mechanics
{
    public sealed class ZombyKillCountEventMechanic : IEventMechanic
    {
        private readonly ZombyModel zombyModel;

        public ZombyKillCountEventMechanic(ZombyModel zombyModel)
        {
            this.zombyModel = zombyModel;
        }

        private void OnDeath()
        {
            zombyModel.updateKillCount.Invoke(zombyModel);
        }

        public void OnEnable()
        {
            zombyModel.onDeath.Subscribe(OnDeath);
        }

        public void OnDisable()
        {
            zombyModel.onDeath.UnSubscribe(OnDeath);
        }
    }
}
