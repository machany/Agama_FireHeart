using Agama.Scripts.Entities;
using GGMPool;
using Scripts.Combat;
using Scripts.Core.Sound;
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

        [Header("Sound")]
        [SerializeField] private PoolManagerSO _poolManager;
        [SerializeField] private SoundSO _explosion;
        [SerializeField] private PoolTypeSO _soundPlayer;
        private Animator _animator;
        private Action OnAnimationEventEvent;

        private int hash;

        protected override void Awake()
        {
            base.Awake();

            hash = Animator.StringToHash(explosion);

            _animator = transform.GetComponent<Animator>();
            _damageCaster.InitCaster(null);

            _rbCompo.linearDamping = dragPower;
            _rbCompo.angularDamping = angularDragPower;
        }

        public override void Init(Vector2 dir, Vector3 pos, float damage, Entity owner)
        {
            transform.right = dir;
            transform.rotation = Quaternion.Euler(0, 0, 180 * (0.5f + (0.5f * Mathf.Sign(dir.x))));
            transform.position = pos;
            _damage = damage;
            _rbCompo.linearVelocity = Vector2.zero;
            _rbCompo.AddForceAtPosition(dir.normalized * bulletSpeed, forcePosition.position, ForceMode2D.Impulse);
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

            _rbCompo.linearVelocity = Vector2.zero;
            _rbCompo.angularVelocity = 0;
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
            var sp = _poolManager.Pop(_soundPlayer) as SoundPlayer;
            sp.PlaySound(_explosion,transform.position);
            _damageCaster.CastDamage(_damage);
        }

        private void FixedUpdate()
        {
            _rbCompo.angularVelocity = Mathf.Clamp(_rbCompo.angularVelocity, -maxAnguler, maxAnguler);
        }
    }
}
