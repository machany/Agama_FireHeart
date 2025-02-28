using Agama.Scripts.Events;
using Scripts.Core;
using System;
using UnityEngine;

namespace Scripts.GameSystem
{
    public class TimeManager : MonoBehaviour
    {
        private static TimeManager _instance;
        public static TimeManager Instance => _instance;

        private float _currentTime;
        [SerializeField] private int _morningMinutes;
        [SerializeField] private int _eveningMinutes;
        public float currentSec => IsNight.Value ? _eveningMinutes * 60 : _morningMinutes * 60;
        public float PastTime => (IsNight.Value ? _morningMinutes * 60 : 0) + _currentTime;
        public float DayTime => _eveningMinutes * 60 + _morningMinutes* 60;
        public NotifyValue<bool> IsNight { get; private set; }
        public NotifyValue<int> DayCount { get; private set; }
        private void Awake()
        {
            DayCount = new NotifyValue<int>();
            IsNight = new NotifyValue<bool>();
            IsNight.OnValueChanged += HandleTimeChanged;
            if (_instance == null)
                _instance = this;
            else
                Debug.LogWarning("TimeManager is already existing");
        }

        private void HandleTimeChanged(bool prev, bool next)
        {
            if (!next)
                DayCount.Value++;
        }

        private void Update()
        {
            _currentTime += Time.deltaTime;
            if (_currentTime > currentSec)
            {
                _currentTime = 0;
                IsNight.Value = !IsNight.Value;
            }
        }
    }
}
