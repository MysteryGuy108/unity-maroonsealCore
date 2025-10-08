using System;
using System.Collections.Generic;

using UnityEngine;


namespace MaroonSeal
{
    [System.Serializable]
    public class EventBasedValue<TValue> : ISerializationCallbackReceiver
    {
        [SerializeField] private TValue current;
        private TValue previous;

        private HashSet<Action<TValue>> callbackLUT;

        public TValue Value
        {
            get { return current; }
            private set
            {
                previous = current;
                current = value;
                InvokeCallbacks(current);
            }
        }

        public TValue Previous => previous;

        #region Constructors
        public EventBasedValue(TValue _value) {
            current = _value;
            previous = _value;
            callbackLUT = new();
        }
        public EventBasedValue() : this(default) {}

        ~EventBasedValue() => callbackLUT?.Clear();
        #endregion

        #region Callbacks
        public void AddCallback(Action<TValue> _value)
        {
            if (callbackLUT.Contains(_value)) { return; }
            callbackLUT.Add(_value);
        }

        public void RemoveCallback(Action<TValue> _value)
        {
            if (!callbackLUT.Contains(_value)) { return; }
            callbackLUT.Remove(_value);
        }

        private void InvokeCallbacks(TValue _value)
        {
            foreach (Action<TValue> callback in callbackLUT)
            {
                callback.Invoke(_value);
            }
        }
        #endregion

        #region ISerializationCallbackReceiver
        public void OnAfterDeserialize()
        {
            if (callbackLUT == null) { return; }
            if (current.Equals(previous)) { return; }

            previous = current;
            InvokeCallbacks(current);
        }

        public void OnBeforeSerialize() {}
        #endregion
    }
}

