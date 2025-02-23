using Agama.Scripts.Entities;
using Library;
using System.Collections.Generic;
using UnityEngine;

namespace Agama.Scripts.Combats.DamageCasters
{
    // 과연 MonoBehaviour에서 제네릭을 SerializedField처럼 가져올 방법이 없었을까..?
    sealed public class TargetingDamageCaster : VisualFieldDiscriminationDamageCaster
    {
        [Header("Targeting Mark")]
        [SerializeField] private Transform _targetingMark;
        [SerializeField] private Sprite targetingMarkSprite;

        private List<Collider2D> _hitResultList;
        private IDamageable _target;

        public override void InitCaster(Entity owner)
        {
            base.InitCaster(owner);

            _hitResultList = new List<Collider2D>();

            _targetingMark ??= new GameObject().transform;
            _targetingMark.SetComponent<SpriteRenderer>(spriteRenderer =>
                {
                    spriteRenderer.enabled = true;
                    spriteRenderer.sprite = targetingMarkSprite;
                    spriteRenderer.color = Color.white;
                });
            _targetingMark.gameObject.SetActive(false);
        }

        public override void UpdateCaster()
        {
            base.UpdateCaster();

            FindTarget();
        }

        private void FindTarget()
        {
            Physics2D.OverlapCircle(transform.transform.position, senceRange, contactFilter, _hitResultList);

            if (_hitResultList.Count > 0)
                foreach (Collider2D target in _hitResultList)
                    if (ViaualFieldDiscrimination(target.transform))
                        return;

            _targetingMark.gameObject.SetActive(false);
            _target = null;
        }

        private bool ViaualFieldDiscrimination(Transform target)
        {
            Vector2 forTargetDirection = (Vector2)target.position - (Vector2)transform.position;
            float forTargetAngle = Vector2.Dot(transform.up.normalized, forTargetDirection.normalized); // f^ * v^ 내적 (f = 플래이어 정면 방향벡터, v = 타겟까지의 방향벡터)

            if (forTargetAngle >= _viewAngle && target.TryGetComponent(out IDamageable damageable))
            {
                if (_target != damageable)
                {
                    _targetingMark.gameObject.SetActive(true);

                    _target = damageable;
                    _targetingMark.rotation = Quaternion.identity;
                }

                _targetingMark.position = target.position;

                return true;
            }

            return false;
        }

        public override bool CastDamage(float damage)
        {
            _target?.ApplyDamage(_currentDamageType, damage, _owner);

            return _target != null;
        }
    }
}
