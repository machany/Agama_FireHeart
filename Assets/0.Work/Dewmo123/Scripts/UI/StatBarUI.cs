using DG.Tweening;
using Scripts.Stats;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class StatBarUI : MonoBehaviour
    {
        [SerializeField] private Stat _stat;
        [SerializeField]  private Image _image;
        [SerializeField] private float _duration;
        [SerializeField] private TextMeshProUGUI _text;
        public void Start()
        {
            _text.text = $"{_stat.currentStat.Value} / {_stat.maxStat}";
            _stat.currentStat.OnValueChanged += HandleStatChanged;
        }

        private void HandleStatChanged(float prev, float next)
        {
            _image.DOKill();
            _image.DOFillAmount(_stat.StatPercent, _duration);
            _text.text = $"{next} / {_stat.maxStat}";
        }
    }
}
