using Agama.Scripts.Entities;
using GGMPool;
using Scripts.Combat;
using System.Linq;
using UnityEngine;

namespace Scripts.Structures
{
    public class Cannon : Structure
    {
        [Header("Attack Setting")]
        [SerializeField] private float _attackRad;
        [SerializeField] private LayerMask _targetLayer;
        [SerializeField] private float _attackDelay;
        [SerializeField] private Transform _firePos;

        [SerializeField] private PoolTypeSO _bulletType;
        [SerializeField] private PoolManagerSO _poolManager;
        private float _curTime;
        private Collider2D _target;
        private void Attack() 
        {
            _target = Physics2D.OverlapCircle(transform.position, _attackRad, _targetLayer);

            if (_target == null) return;

            Vector2 dir = _target.transform.position - transform.position;
            var bullet = _poolManager.Pop(_bulletType) as Bullet;
            bullet.Init(dir.normalized,_firePos.position);
        }
        private void Update()
        {
            _curTime += Time.deltaTime;
            if (_curTime >= _attackDelay)
            {
                Attack();
                _curTime = 0;
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _attackRad);
        }
#endif
    }
}
