using System.Collections;
using UnityEngine;

namespace Scripts.Combat
{
    public class Bullet : MonoBehaviour
    {
        private Rigidbody2D _rbCompo;
        [SerializeField] private float _bulletSpeed;
        public void Init(Vector2 dir)
        {
            _rbCompo = GetComponent<Rigidbody2D>();
            transform.right = dir;
            _rbCompo.linearVelocity = dir * _bulletSpeed;
        }
    }
}