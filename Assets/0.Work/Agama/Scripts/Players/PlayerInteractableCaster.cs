using Agama.Scripts.Entities;
using Scripts.Structures;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Agama.Scripts.Players
{
    sealed public class PlayerInteractableCaster : MonoBehaviour, IEntityComponent
    {
        [Header("Contact Filter Setting")]
        [SerializeField] private ContactFilter2D contactFilter;
        [Header("Visual Field Discrimination Setting")]
        [SerializeField] private float senceRange;
        [Range(0f, 360f)]
        [SerializeField] private float fieldOfViewAngle;

        private List<Collider2D> _hitResultList = new List<Collider2D>();
        private Player _player;
        private IInteractable _target;

        private float _viewAngle;

        public void Initialize(Entity owner)
        {
            _viewAngle = Mathf.Cos(fieldOfViewAngle / 2 * Mathf.Deg2Rad);
            _player = owner as Player;
            Debug.Assert(_player != null, "can find Player");

            _player.InputSO.OnInteractKeyPressedEvent += Interact;
        }

        private void OnDestroy()
        {
            _player.InputSO.OnInteractKeyPressedEvent -= Interact;
        }

        private void Update()
        {
            FindTarget();

            //if (_player.InputSO.PreviousInputVector.magnitude <= Mathf.Epsilon)
            //    transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, -90 * _renderer.FacingDirection);
            //else
            transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, Mathf.Atan2(-_player.InputSO.PreviousInputVector.x, _player.InputSO.PreviousInputVector.y) * (180 / Mathf.PI));
        }

        private void FindTarget()
        {
            Physics2D.OverlapCircle(transform.transform.position, senceRange, contactFilter, _hitResultList);

            if (_hitResultList.Count > 0)
                foreach (Collider2D target in _hitResultList)
                    if (ViaualFieldDiscrimination(target.transform))
                        return;

            _target = null;
        }

        private bool ViaualFieldDiscrimination(Transform target)
        {
            Vector2 forTargetDirection = (Vector2)target.position - (Vector2)transform.position;
            float forTargetAngle = Vector2.Dot(transform.up.normalized, forTargetDirection.normalized); // f^ * v^ 내적 (f = 플래이어 정면 방향벡터, v = 타겟까지의 방향벡터)

            if (forTargetAngle >= _viewAngle && target.TryGetComponent(out IInteractable interactable))
            {
                if (_target != interactable)
                    _target = interactable;

                return true;
            }

            return false;
        }

        private void Interact()
            => _target?.Interact();

#if UNITY_EDITOR
        [Header("Draw Color Setting")]
        [SerializeField] private Color drawColor = Color.red;

        private void OnDrawGizmosSelected()
        {
            Handles.color = drawColor;
            Handles.DrawSolidArc(transform.position, Vector3.back, Quaternion.Euler(0, 0, fieldOfViewAngle / 2) * transform.up, fieldOfViewAngle, senceRange);

            Gizmos.color = drawColor;
            Gizmos.DrawWireSphere(transform.position, senceRange);
        }
#endif
    }
}
