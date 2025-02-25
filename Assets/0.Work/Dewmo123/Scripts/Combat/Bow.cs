using Agama.Scripts.Entities;
using GGMPool;
using Scripts.Map;
using Scripts.Structures;
using System;
using UnityEngine;

namespace Scripts.Combat
{
    public class Bow : MonoBehaviour, IEntityComponent
    {
        private Cannon _cannon;
        private Collider2D _target;
        private StructureAnimator _anim;

        [SerializeField] private Transform _firePos;

        [SerializeField] private PoolTypeSO _bulletType;
        [SerializeField] private PoolManagerSO _poolManager;
        private Vector2 _dir;
        public void Initialize(Entity owner)
        {
            _cannon = owner as Cannon;
            _anim = owner.GetComp<StructureAnimator>();
            _cannon.OnAttack += HandleAttackEvent;
            _cannon.GetComp<EntityAnimatorTrigger>().OnAnimationEvent += HandleAttackTrigger;
        }


        private void OnDestroy()
        {
            _cannon.GetComp<EntityAnimatorTrigger>().OnAnimationEvent -= HandleAttackTrigger;
            _cannon.OnAttack -= HandleAttackEvent;
        }
        private void HandleAttackTrigger()
        {
            var arrow = _poolManager.Pop(_bulletType) as Bullet;
            arrow.Init(_dir, _firePos.position);
        }
        private void HandleAttackEvent()
        {
            _anim.PlayAnimation("Attack");
        }

        private void Update()
        {
            _target = _cannon.target;
            _dir = _target == null ? transform.right : _target.transform.position - transform.position;
            transform.right = _dir;
        }
    }
}
