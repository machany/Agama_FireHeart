using Agama.Scripts.Entities;
using GGMPool;
using System.Collections;
using UnityEngine;

namespace Scripts.Combat
{
    public abstract class Projectile : MonoBehaviour, IPoolable
    {
        [SerializeField] protected float bulletSpeed;
        [SerializeField] protected float duration;
        [SerializeField] protected PoolTypeSO myType;

        public PoolTypeSO PoolType => myType;
        public GameObject GameObject => gameObject;

        protected Rigidbody2D _rbCompo;
        protected Pool _myPool;

        protected float _damage;

        protected virtual void Awake()
        {
            _rbCompo = GetComponent<Rigidbody2D>();
        }

        public virtual void Init(Vector2 dir, Vector3 pos)
        {
            transform.position = pos;

            dir.Normalize();
            transform.right = dir;
            _rbCompo.linearVelocity = dir * bulletSpeed;

            StartCoroutine(ReturnToPool());
        }

        public virtual IEnumerator ReturnToPool()
        {
            yield return new WaitForSeconds(duration);
            _myPool.Push(this);
        }

        public void ResetItem()
        {
            _rbCompo.linearVelocity = Vector2.zero;
        }

        public void SetUpPool(Pool pool)
        {
            _myPool = pool;
        }

        public void SetDamage(float damage)
        {
            _damage = damage;
        }
    }
}
