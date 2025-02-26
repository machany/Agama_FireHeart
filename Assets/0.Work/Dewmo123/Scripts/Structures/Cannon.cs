using Agama.Scripts.Entities;
using GGMPool;
using Scripts.Combat;
using System;
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

        public Action OnAttack;

        private float _curTime;
        public Collider2D target { get; private set; }
        private void Update()
        {
            _curTime += Time.deltaTime;
            if (_curTime >= _attackDelay)
            {
                target = Physics2D.OverlapCircle(transform.position, _attackRad, _targetLayer);
                if (target != null)
                    OnAttack?.Invoke();
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
