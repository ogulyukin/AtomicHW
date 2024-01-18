using System;

namespace Core
{
    [Serializable]
    public sealed class AtomicEvent<T>
    {
        private event Action<T> OnEvent;

        public void Invoke(T args)
        {
            OnEvent?.Invoke(args);
        }

        public void Subscribe(Action<T> action)
        {
            OnEvent += action;
        }

        public void UnSubscribe(Action<T> action)
        {
            OnEvent -= action;
        }
    }
    
    [Serializable]
    public sealed class AtomicEvent
    {
        private event Action OnEvent;

        public void Invoke()
        {
            OnEvent?.Invoke();
        }

        public void Subscribe(Action action)
        {
            OnEvent += action;
        }

        public void UnSubscribe(Action action)
        {
            OnEvent -= action;
        }
    }
}
