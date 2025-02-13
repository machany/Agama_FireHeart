using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Agama.Scripts.Core
{
    [CreateAssetMenu(fileName = "StatSO", menuName = "SO/StatSystem/Stat", order = 0)]
    public class StatSO : ScriptableObject, ICloneable
    {
        public delegate void ValueChangeHandler(StatSO stat, int current, int previous);
        public event ValueChangeHandler OnValueChange;

        public string statName;
        [TextArea]
        public string description;

        [SerializeField] private int baseValue, minValue, maxValue;

        private Dictionary<object, int> _modifyDictionary = new Dictionary<object, int>();

        private float _modifiedValue = 0;

        #region Property section

        public int MaxValue
        {
            get => maxValue;
            set => maxValue = value;
        }

        public int MinValue
        {
            get => minValue;
            set => minValue = value;
        }

        public int Value => (int)Mathf.Clamp(baseValue + _modifiedValue, MinValue, MaxValue);
        public bool IsMax => Mathf.Approximately(Value, MaxValue);
        public bool IsMin => Mathf.Approximately(Value, MinValue);

        public int BaseValue
        {
            get => baseValue;
            set
            {
                int prevValue = Value;
                baseValue = Mathf.Clamp(value, MinValue, MaxValue);
                TryInvokeValueChangedEvent(Value, prevValue);
            }
        }

        #endregion

        public void AddModifier(object key, int value)
        {
            if (_modifyDictionary.ContainsKey(key)) return;
            int prevValue = Value;

            _modifiedValue += value;
            _modifyDictionary.Add(key, value);

            TryInvokeValueChangedEvent(Value, prevValue);
        }

        public void RemoveModifier(object key)
        {
            if (_modifyDictionary.TryGetValue(key, out int value))
            {
                int prevValue = Value;
                _modifiedValue -= value;
                _modifyDictionary.Remove(key);

                TryInvokeValueChangedEvent(Value, prevValue);
            }
        }

        public void ClearAllModifier()
        {
            int prevValue = Value;
            _modifyDictionary.Clear();
            _modifiedValue = 0;
            TryInvokeValueChangedEvent(Value, prevValue);
        }

        private void TryInvokeValueChangedEvent(int current, int prevValue)
        {
            if (Mathf.Approximately(current, prevValue) == false)
                OnValueChange?.Invoke(this, current, prevValue);
        }

        public object Clone() => Instantiate(this);

    }
}
