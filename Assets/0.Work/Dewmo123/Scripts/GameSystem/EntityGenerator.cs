using Agama.Scripts.Enemies;
using GGMPool;
using System;
using System.Collections.Generic;
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
    public abstract class EntityGenerator : MonoBehaviour
    {
        [SerializeField] protected Vector2 _ignoreRegion;
        [SerializeField] protected Vector2 _spawnRegion;
        [SerializeField] protected List<SpawnRatio> _enemyTypes;
        [SerializeField] protected PoolManagerSO _poolManager;
        [SerializeField] protected Transform _center;

        public virtual BehaviorEnemy GenerateEnemy()
        {
            float x = Random.Range(_ignoreRegion.x, _spawnRegion.x);
            float y = Random.Range(_ignoreRegion.y, _spawnRegion.y);

            x = ((int)(x * 100) % 2 == 1) ? -x : x;
            y = ((int)(y * 100) % 2 == 1) ? -y : y;

            var type = GetRandomType(_enemyTypes);
            var enemy = _poolManager.Pop(type) as BehaviorEnemy;
            enemy.transform.position = _center.position + new Vector3(x, y);
            return enemy;
        }
        private PoolTypeSO GetRandomType(List<SpawnRatio> list)
        {
            Random.InitState((int)(Random.value * 100));
            float currentRatio = 0;
            float ratio = Random.value;

            foreach (var item in list)
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
