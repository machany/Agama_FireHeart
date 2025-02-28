using Scripts.Core;
using Scripts.GameSystem;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class TimeInfoUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI dayCntTxt;
        [SerializeField] private RectTransform remainTimeMark;
        private NotifyValue<bool> _isNight;
        private void Start()
        {
            TimeManager.Instance.DayCount.OnValueChanged += HandleDayChanged;
            dayCntTxt.text = "0";
            remainTimeMark.rotation = Quaternion.identity;
        }
        private void OnDestroy()
        {
            TimeManager.Instance.DayCount.OnValueChanged -= HandleDayChanged;
        }
        private void Update()
        {
            float maxTime = TimeManager.Instance.DayTime;
            float current = TimeManager.Instance.PastTime;

            float rotation = Mathf.Lerp(0, 180, current / maxTime);
            remainTimeMark.rotation = Quaternion.Euler(0, 0, rotation);
        }
        private void HandleDayChanged(int prev, int next)
        {
            dayCntTxt.text = $"{next}";
        }
    }
}
