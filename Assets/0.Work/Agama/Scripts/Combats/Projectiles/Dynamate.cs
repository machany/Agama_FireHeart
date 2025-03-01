using Agama.Scripts.Entities;
using GGMPool;
using Scripts.Combat;
using System;
using System.Collections;
using UnityEngine;

namespace Agama.Scripts.Combats.Projectiles
{
    public class Dynamate : Projectile
    {
        [Header("Dynamate")]
        [SerializeField] private Transform forcePosition;
        [SerializeField] private PoolManagerSO poolManager;
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
            _damageCaster.InitCaster(null);

            _rid.linearDamping = dragPower;
            _rid.angularDamping = angularDragPower;
        }

        public override void Init(Vector2 dir, Vector3 pos, float damage, Entity owner)
        {
            transform.right = dir;
            transform.rotation = Quaternion.Euler(0, 0, 180 * (0.5f + (0.5f * Mathf.Sign(dir.x))));
            transform.position = pos;

            _rid.linearVelocity = Vector2.zero;
            _rid.AddForceAtPosition(dir.normalized * bulletSpeed, forcePosition.position, ForceMode2D.Impulse);
            _animator.SetBool(hash, false);

            _damageCaster.SetOwner(owner);

            StartCoroutine(ReturnToPool());
        }

        protected override void DeadProjectile()
        {
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
            _damageCaster.CastDamage(_damage);
        }

        private void FixedUpdate()
        {
            _rid.angularVelocity = Mathf.Clamp(_rid.angularVelocity, -maxAnguler, maxAnguler);
        }
    }
}
