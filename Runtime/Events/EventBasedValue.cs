using System;
using UnityEngine;


namespace MaroonSeal
{
    public class EventBasedValue<TValue>
    {
        private TValue current;
        private TValue previous;

        public TValue Value
        {
            get { return current; }
            private set
            {
                current = value;
                OnValueChanged?.Invoke(current);
            }
        }

        public TValue PreviousValue => previous;
        public event Action<TValue> OnValueChanged;
    }
}

