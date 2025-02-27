using Scripts.Core;
using Scripts.GameSystem;
using System;
using TMPro;
using UnityEngine;

namespace Scripts.UI
{
    public class TimeInfoUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _dayCntTxt;
        [SerializeField] private TextMeshProUGUI _remainTimeTxt;
        private NotifyValue<bool> _isNight;
        private void Start()
        {
            TimeManager.Instance.DayCount.OnValueChanged += HandleDayChanged;
            _dayCntTxt.text = "Day : 0";
        }
        private void OnDestroy()
        {
            TimeManager.Instance.DayCount.OnValueChanged -= HandleDayChanged;
        }
        private void Update()
        {
            int remain = TimeManager.Instance.RemainTime;
            int sec = remain % 60;
            int min = remain / 60;
            _remainTimeTxt.text = $"{min} : {sec}";
        }
        private void HandleDayChanged(int prev, int next)
        {
            _dayCntTxt.text = $"Day : {next}";
        }
    }
}
