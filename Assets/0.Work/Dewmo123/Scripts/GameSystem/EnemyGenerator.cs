using Agama.Scripts.Enemies;
using Agama.Scripts.Entities;
using GGMPool;
using System;
using System.Collections.Generic;
using Unity.AppUI.UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.GameSystem
{
    [Serializable]
    public struct SpawnRatio
    {
        public PoolTypeSO type;
        [Range(0, 1)]
        public float ratio;
    }
    public class EnemyGenerator : MonoBehaviour
    {
        private Transform _center;
        [SerializeField] private Vector2 _ignoreRegion;
        [SerializeField] private Vector2 _spawnRegion;
        [SerializeField] private List<SpawnRatio> _enemyTypes;
        [SerializeField] private PoolManagerSO _poolManager;

        [SerializeField] private int _phaseEnemyCount;
        [SerializeField] private float _enemyIncrementPerDay;
        private void Awake()
        {
            _center = GameObject.FindGameObjectWithTag("Player").transform;
        }
        private void Start()
        {
            TimeManager.Instance.IsNight.OnValueChanged += HandleNightEvent;
        }

        private void HandleNightEvent(bool prev, bool next)
        {
            if (next)
                for (int i = 0; i < _phaseEnemyCount; i++)
                    GenerateEnemy();
            else
                _phaseEnemyCount = (int)(_phaseEnemyCount * _enemyIncrementPerDay);
        }

        [ContextMenu("Generate")]
        public void GenerateEnemy()
        {
            float x = Random.Range(_ignoreRegion.x, _spawnRegion.x);
            float y = Random.Range(_ignoreRegion.y, _spawnRegion.y);

            x = ((int)(x * 100) % 2 == 1) ? -x : x;
            y = ((int)(y * 100) % 2 == 1) ? -y : y;

            var type = GetRandomType();
            var enemy = _poolManager.Pop(type) as BehaviorEnemy;
            enemy.transform.position = _center.position + new Vector3(x, y);
        }
        private PoolTypeSO GetRandomType()
        {
            Random.InitState((int)(Random.value * 100));
            float currentRatio = 0;
            float ratio = Random.value;

            foreach (var item in _enemyTypes)
            {
                currentRatio += item.ratio;
                if (currentRatio > ratio)
                    return item.type;
            }
            Debug.LogWarning("total ratio is not 1");
            return null;
        }
#if UNITY_EDITOR

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(_center.position, _ignoreRegion * 2);
            Gizmos.DrawWireCube(_center.position, _spawnRegion * 2);
        }
#endif
    }
}
