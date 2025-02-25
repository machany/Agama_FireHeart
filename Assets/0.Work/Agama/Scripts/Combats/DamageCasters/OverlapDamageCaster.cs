using Agama.Scripts.Entities;
using System;
using UnityEngine;

namespace Agama.Scripts.Combats.DamageCasters
{
    public class OverlapDamageCaster : DamageCaster
    {
        public enum OverlapCastType
        {
            Circle,
            Box
        }
        [SerializeField] protected OverlapCastType overlapCastType;
        [SerializeField] private Vector2 damageBoxSize;
        [SerializeField] private float damageRadius;

        private Func<int> GetOverlapCountFunc;
        private Collider2D[] _hitResults;

        public override void InitCaster(Entity owner)
        {
            base.InitCaster(owner);
            _hitResults = new Collider2D[maxHitCount];
            GetOverlapCountFunc = overlapCastType switch
            {
                OverlapCastType.Circle => () => Physics2D.OverlapCircle(transform.position, damageRadius, contactFilter, _hitResults),
                OverlapCastType.Box => () => Physics2D.OverlapBox(transform.position, damageBoxSize, 0, contactFilter, _hitResults),
                _ => throw new ArgumentNullException()
            };
        }

        public override bool CastDamage(float damage)
        {
            int cnt = overlapCastType switch
            {
                OverlapCastType.Circle =>  Physics2D.OverlapCircle(transform.position, damageRadius, contactFilter, _hitResults),
                OverlapCastType.Box =>  Physics2D.OverlapBox(transform.position, damageBoxSize, 0, contactFilter, _hitResults),
                _ => throw new ArgumentNullException()
            };

            for (int i = 0; i < cnt; i++)
            {
                if (_hitResults[i].TryGetComponent(out IDamageable damageable))
                {
            Debug.Log("ss");
                    damageable.ApplyDamage(_currentDamageType, damage, _owner);
                }
            }

            return cnt > 0;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            switch (overlapCastType)
            {
                case OverlapCastType.Circle:
                    Gizmos.DrawWireSphere(transform.position, damageRadius);
                    break;
                case OverlapCastType.Box:
                    Gizmos.DrawWireCube(transform.position, damageBoxSize);
                    break;
            }
        }
#endif
    }
}
