using System;
using UnityEngine;

namespace Core
{
    [Serializable]
    public sealed class AtomicVariable<T>
    {
        [SerializeField] private T value;

        private event Action<T> OnValueChanged;
        
        public T Value
        {
            get => value;
            set
            {
                this.value = value;
                OnValueChanged?.Invoke(value);
            } 
        }

        public void Subscribe(Action<T> action)
        {
            OnValueChanged += action;
        }
        
        public void UnSubscribe(Action<T> action)
        {
            OnValueChanged -= action;
        }
    }
}
