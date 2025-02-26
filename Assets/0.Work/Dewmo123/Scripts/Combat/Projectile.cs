using Agama.Scripts.Combats.DamageCasters;
using Agama.Scripts.Entities;
using GGMPool;
using System.Collections;
using UnityEngine;

namespace Scripts.Combat
{
    public abstract class Projectile : MonoBehaviour, IPoolable
    {
        private Rigidbody2D _rbCompo;
        [SerializeField] private float _bulletSpeed;
        [SerializeField] private float _duration;
        [SerializeField] private PoolTypeSO _myType;
        protected DamageCaster _damageCaster;

        protected float _damage;
        protected Pool _myPool;
        public PoolTypeSO PoolType => _myType;
        protected Entity _entity;

        public GameObject GameObject => gameObject;
        protected virtual void Awake()
        {
            _rbCompo = GetComponent<Rigidbody2D>();
            _damageCaster = GetComponentInChildren<DamageCaster>();
            _damageCaster.InitCaster(_entity);
        }
        public virtual void Init(Vector2 dir, Vector3 pos,float damage,Entity entity)
        {
            _entity = entity;
            _damage = damage;
            transform.position = pos;
            transform.right = dir;
            _rbCompo.linearVelocity = dir * _bulletSpeed;
            StartCoroutine(ReturnToPool());
        }
        public IEnumerator ReturnToPool()
        {
            yield return new WaitForSeconds(_duration);
            _myPool.Push(this);
        }
        public void ResetItem()
        {
            _rbCompo.linearVelocity = Vector2.zero;
        }
        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            _damageCaster.CastDamage(_damage);
            _myPool.Push(this);
        }
        public void SetUpPool(Pool pool)
        {
            _myPool = pool;
        }
    }
}
