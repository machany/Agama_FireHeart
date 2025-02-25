using Agama.Scripts.Combats.DamageCasters;
using GGMPool;
using Scripts.Core.Sound;
using System.Collections;
using UnityEngine;

namespace Scripts.Combat
{
    public class Bullet : Projectile
    {
        protected override void Awake()
        {
            base.Awake();

        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            _damageCaster.CastDamage(_damage);
            _myPool.Push(this);
        }
    }
}