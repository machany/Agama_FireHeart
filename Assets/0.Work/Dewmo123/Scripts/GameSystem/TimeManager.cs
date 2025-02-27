using Agama.Scripts.Events;
using Scripts.Core;
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
        public NotifyValue<bool> IsNight { get; private set; }
        private void Awake()
        {
            IsNight = new NotifyValue<bool>();
            if (_instance == null)
                _instance = this;
            else
                Debug.LogWarning("TimeManager is already existing");
        }
        private void Update()
        {
            _currentTime += Time.deltaTime;
            if ((!IsNight.Value && _currentTime >= _morningMinutes) || (IsNight.Value && _currentTime >= _eveningMinutes))
            {
                _currentTime = 0;
                IsNight.Value = !IsNight.Value;
            }
        }
    }
}
