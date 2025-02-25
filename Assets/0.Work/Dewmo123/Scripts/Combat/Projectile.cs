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
        private Pool _myPool;
        public PoolTypeSO PoolType => _myType;

        public GameObject GameObject => gameObject;
        private void Awake()
        {
            _rbCompo = GetComponent<Rigidbody2D>();
        }
        public virtual void Init(Vector2 dir, Vector3 pos)
        {
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

        public void SetUpPool(Pool pool)
        {
            _myPool = pool;
        }
    }
}
