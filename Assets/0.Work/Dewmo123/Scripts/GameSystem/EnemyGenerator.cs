using Agama.Scripts.Enemies;
using Agama.Scripts.Entities;
using GGMPool;
using Scripts.Core;
using System;
using System.Collections.Generic;
using Unity.AppUI.UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.GameSystem
{

    public class EnemyGenerator : EntityGenerator
    {
        [SerializeField] private int _phaseEnemyCount;
        [SerializeField] private float _enemyIncrementPerDay;
        [SerializeField] private float _enemySpawnDelay;

        private NotifyValue<bool> _isNight;
        private float _currentTime;
        private void Start()
        {
            _isNight = TimeManager.Instance.IsNight;
            _isNight.OnValueChanged += HandleNightEvent;
        }
        private void OnDestroy()
        {
            _isNight.OnValueChanged -= HandleNightEvent;
        }
        private void Update()
        {
            if (!_isNight.Value) return;
            _currentTime += Time.deltaTime;
            if (_currentTime > _enemySpawnDelay)
            {
                GenerateEnemy();
                _currentTime = 0;
            }
        }
        private void HandleNightEvent(bool prev, bool next)
        {
            if (next)
                for (int i = 0; i < _phaseEnemyCount; i++)
                    GenerateEnemy();
            else
                _phaseEnemyCount = (int)(_phaseEnemyCount * _enemyIncrementPerDay);
        }
    }
}
