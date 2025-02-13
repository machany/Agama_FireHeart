using Agama.Scripts.Entities;
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

        [SerializeField] private GameObject _bulletPrefab;

        private float _curTime;
        private Collider2D _target;
        private void Attack()
        {
            _target = Physics2D.OverlapCircle(transform.position, _attackRad, _targetLayer);

            if (_target == null) return;

            Vector2 dir = _target.transform.position - transform.position;
            var bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity).GetComponent<Bullet>();
            bullet.Init(dir.normalized);
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
        protected override void HandleDeadEvent()
        {
        }

        protected override void HandleHitEvent()
        {
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
