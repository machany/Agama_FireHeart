using Agama.Scripts.Entities;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Agama.Scripts.Combats.DamageCasters
{
    public class VisualFieldDiscriminationDamageCaster : DamageCaster
    {
        [Header("Visual Field Discrimination Setting")]
        [SerializeField] protected float senceRange;
        [Range(0f, 360f)]
        [SerializeField] protected float fieldOfViewAngle;

        private Collider2D[] _hitResults;

        protected float _viewAngle;

        public override void InitCaster(Entity owner)
        {
            base.InitCaster(owner);

            _hitResults = new Collider2D[maxHitCount];
            _viewAngle = Mathf.Cos(fieldOfViewAngle / 2 * Mathf.Deg2Rad); // 2D라 Mathf.Deg2Rad를 곱함
        }

        public override bool CastDamage(float damage)
        {
            int count = Physics2D.OverlapCircle(transform.transform.position, senceRange, contactFilter, _hitResults);

            if (count > 0)
            {
                foreach (Collider2D target in _hitResults)
                {
                    Vector2 forTargetDirection = (Vector2)target.transform.position - (Vector2)transform.position;
                    float forTargetAngle = Vector2.Dot(transform.up.normalized, forTargetDirection.normalized); // f^ * v^ 내적 (f = 플래이어 정면 방향벡터, v = 타겟까지의 방향벡터)

                    if (forTargetAngle >= _viewAngle && target.TryGetComponent(out IDamageable damageable))
                        damageable.ApplyDamage(_currentDamageType, damage, _owner);
                }
                return true;
            }

            return false;
        }

#if UNITY_EDITOR
        [Header("Draw Color Setting")]
        [SerializeField] private Color drawColor = Color.red;

        protected virtual void OnDrawGizmosSelected()
        {
            Handles.color = drawColor;
            Handles.DrawSolidArc(transform.position, Vector3.back, Quaternion.Euler(0, 0, fieldOfViewAngle / 2) * transform.up, fieldOfViewAngle, senceRange);

            Gizmos.color = drawColor;
            Gizmos.DrawWireSphere(transform.position, senceRange);
        }
#endif
    }
}
