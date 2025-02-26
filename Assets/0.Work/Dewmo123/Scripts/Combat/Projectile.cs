using Agama.Scripts.Combats.DamageCasters;
using Agama.Scripts.Entities;
using GGMPool;
using System.Collections;
using UnityEngine;

namespace Scripts.Combat
{
    public abstract class Projectile : MonoBehaviour, IPoolable
    {
        [SerializeField] protected float bulletSpeed, duration;
        [SerializeField] private LayerMask targetLayer;
        [SerializeField] protected PoolTypeSO myType;

        protected float _damage;

        protected Pool _myPool;
        public PoolTypeSO PoolType => myType;

        protected Rigidbody2D _rbCompo;
        protected Entity _entity;
        protected DamageCaster _damageCaster;

        public GameObject GameObject => gameObject;
        protected virtual void Awake()
        {
            _rbCompo = GetComponent<Rigidbody2D>();
            _damageCaster = GetComponentInChildren<DamageCaster>();
            // _damageCaster.InitCaster(_entity); <- ?? 조기 상태에서는 entity가 null임
             _damageCaster.InitCaster(null);
        }

        public virtual void Init(Vector2 dir, Vector3 pos, float damage, Entity entity)
        {
            _entity = entity;
            _damage = damage;
            transform.position = pos;
            transform.right = dir;
            _rbCompo.linearVelocity = dir.normalized * bulletSpeed;
            _damageCaster.SetOwner(entity);

            StartCoroutine(ReturnToPool());
        }

        protected virtual void DeadProjectile()
        {
            _myPool.Push(this);
        }

        public virtual IEnumerator ReturnToPool()
        {
            yield return new WaitForSeconds(duration);
            DeadProjectile();
        }

        public void ResetItem()
        {
            _rbCompo.linearVelocity = Vector2.zero;
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if ((1 << collision.gameObject.layer & targetLayer) != 0)
            {
                _damageCaster.CastDamage(_damage);
                DeadProjectile();
            }
        }

        public void SetUpPool(Pool pool)
        {
            _myPool = pool;
        }
    }
}
