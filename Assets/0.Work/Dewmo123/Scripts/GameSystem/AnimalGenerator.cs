using Agama.Scripts.Enemies;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.GameSystem
{
    public class AnimalGenerator : EntityGenerator
    {
        [SerializeField] private int _maxCount;
        [SerializeField] private float _spawnTime;
        private int _currentCount;

        private float _currentTime;
        private void Update()
        {
            if (_currentCount == _maxCount) return;
            _currentTime += Time.deltaTime;
            if (_currentTime >= _spawnTime)
            {
                GenerateEnemy();
                _currentTime = 0;
            }
        }
        public override BehaviorEnemy GenerateEnemy()
        {
            var enemy = base.GenerateEnemy();
            enemy.OnDeadEvent.RemoveListener(HandleDeadEvent);
            enemy.OnDeadEvent.AddListener(HandleDeadEvent);
            _currentCount++;
            return enemy;
        }

        private void HandleDeadEvent()
        {
            _currentCount--;
        }
    }
}
