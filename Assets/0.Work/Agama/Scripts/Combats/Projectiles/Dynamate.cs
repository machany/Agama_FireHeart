using Agama.Scripts.Combats.DamageCasters;
using GGMPool;
using Scripts.Combat;
using System;
using System.Collections;
using UnityEditor.EditorTools;
using UnityEngine;

namespace Agama.Scripts.Combats.Projectiles
{
    public class Dynamate : Projectile
    {
        [SerializeField] private Transform forcePosition;
        [SerializeField] private DamageCaster damageCaster;
        [SerializeField] private PoolManagerSO poolManager;
        [SerializeField] private LayerMask targetLayer;
        [SerializeField] private float dragPower;

        [Header("Anguler")]
        [SerializeField] private float angularDragPower;
        [SerializeField] private float maxAnguler;

        [Header("Animator")]
        [SerializeField] private string explosion;

        private Animator _animator;
        private Rigidbody2D _rid;
        private Action OnAnimationEventEvent;

        private int hash;

        protected override void Awake()
        {
            base.Awake();

            hash = Animator.StringToHash(explosion);

            _animator = transform.GetComponent<Animator>();
            _rid = transform.GetComponent<Rigidbody2D>();
            damageCaster.InitCaster(null);

            _rid.linearDamping = dragPower;
            _rid.angularDamping = angularDragPower;
        }

        public override void Init(Vector2 dir, Vector3 pos)
        {
            transform.right = dir;
            transform.rotation = Quaternion.Euler(0, 0, 180 * (0.5f + (0.5f * Mathf.Sign(dir.x))));
            transform.position = pos;

            _rid.linearVelocity = Vector2.zero;
            _rid.AddForceAtPosition(dir.normalized * bulletSpeed, forcePosition.position, ForceMode2D.Impulse);
            _animator.SetBool(hash, false);

            StartCoroutine(ReturnToPool());
        }

        public override IEnumerator ReturnToPool()
        {
            yield return new WaitForSeconds(duration);
            Explosion();
        }

        private void Explosion()
        {
            _animator.SetBool(hash, true);

            _rid.linearVelocity = Vector2.zero;
            _rid.angularVelocity = 0;
            transform.rotation = Quaternion.identity;

            OnAnimationEventEvent += HandleAnimationEventEvent;
        }

        private void AnimationEventEvent()
        {
            OnAnimationEventEvent?.Invoke();
        }

        private void AnimationEndEvent()
        {
            _myPool.Push(this);
        }

        private void HandleAnimationEventEvent()
        {
            OnAnimationEventEvent -= HandleAnimationEventEvent;
            damageCaster.CastDamage(_damage);
        }

        private void FixedUpdate()
        {
            _rid.angularVelocity = Mathf.Clamp(_rid.angularVelocity, -maxAnguler, maxAnguler);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if ((1 << collision.gameObject.layer & targetLayer) != 0)
            {
                Explosion();
            }
        }
    }
}
