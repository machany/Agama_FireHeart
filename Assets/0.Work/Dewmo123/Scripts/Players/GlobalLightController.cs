using DG.Tweening;
using Scripts.GameSystem;
using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Scripts.Players
{
    public class GlobalLightController : MonoBehaviour
    {
        private Light2D _light;
        [SerializeField] private float _duration;
        private void Awake()
        {
            _light = GetComponent<Light2D>();
        }
        private void Start()
        {
            TimeManager.Instance.IsNight.OnValueChanged += HandleTimeChanged;
        }

        private void HandleTimeChanged(bool prev, bool next)
        {
            DOTween.To(() => _light.intensity, x => _light.intensity = x, next?0:1, _duration);
        }
    }
}
