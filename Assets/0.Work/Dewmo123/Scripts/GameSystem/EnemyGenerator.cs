using Agama.Scripts.Entities;
using GGMPool;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.GameSystem
{
    public class EnemyGenerator : MonoBehaviour
    {
        [SerializeField] private Transform _center;
        [SerializeField] private Vector2 _ignoreRegion;
        [SerializeField] private Vector2 _spawnRegion;
        [SerializeField] private List<PoolTypeSO> _enemyTypes;
        [SerializeField] private PoolManagerSO _poolManager;
        
        public void GenerateEnemy()
        {
            float x = Random.Range(_ignoreRegion.x,_spawnRegion.x);
            float y = Random.Range(_ignoreRegion.y,_spawnRegion.y);
            var enemy = _poolManager.Pop(_enemyTypes[Random.Range(0,_enemyTypes.Count)]) as Entity;

        }
#if UNITY_EDITOR

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(_center.position, _ignoreRegion);
            Gizmos.DrawWireCube(_center.position, _spawnRegion);
        }
#endif
    }
}
