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

        readonly private HashSet<Action<TValue>> listenerLUT;

        public TValue Value => current;
        public TValue Previous => previous;

        #region Constructors
        public EventBasedValue(TValue _value) {
            current = _value;
            previous = _value;
            listenerLUT = new();
        }
        public EventBasedValue() : this(default) {}

        ~EventBasedValue() => listenerLUT?.Clear();
        #endregion

        public void SetValue(TValue _value)
        {
            previous = current;
            current = _value;
            InvokeListeners(current);
        }

        #region Callbacks
        public void AddListener(Action<TValue> _value)
        {
            if (listenerLUT.Contains(_value)) { return; }
            listenerLUT.Add(_value);
        }

        public void RemoveListener(Action<TValue> _value)
        {
            if (!listenerLUT.Contains(_value)) { return; }
            listenerLUT.Remove(_value);
        }

        private void InvokeListeners(TValue _value)
        {
            foreach (Action<TValue> listener in listenerLUT)
            {
                listener.Invoke(_value);
            }
        }
        #endregion

        #region ISerializationCallbackReceiver
        public void OnAfterDeserialize()
        {
            if (listenerLUT == null) { return; }
            if (current.Equals(previous)) { return; }

            previous = current;
            InvokeListeners(current);
        }

        public void OnBeforeSerialize() {}
        #endregion
    }
}

