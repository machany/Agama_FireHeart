using System;
using UnityEngine;

namespace Scripts.Core
{
    [Serializable]
    public class NotifyValue<T>
    {
        public delegate void ValueChanged(T prev, T next);
        public ValueChanged OnValueChanged;

        [SerializeField]T _value;

        public NotifyValue()
        {
            _value = default;
        }

        public T Value
        {
            get => _value;
            set
            {
                T before = _value;
                _value = value;
                if ((before == null && value != null) || !before.Equals(_value))
                    OnValueChanged?.Invoke(before, _value);
            }
        }
    }
}
