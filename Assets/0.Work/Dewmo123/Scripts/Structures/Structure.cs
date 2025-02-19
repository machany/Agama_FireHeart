using Agama.Scripts.Combats;
using Agama.Scripts.Entities;
using Scripts.EventChannel;
using System;
using UnityEngine;

namespace Scripts.Structures
{
    public class Structure : Entity, IDamageable
    {
        public IDamageable.DamageMethodType DamageableType => throw new NotImplementedException();

        public event Action<float> OnDamage;

        public void ApplyDamage(IDamageable.DamageMethodType damageType, float damage, bool isPowerAttack)
        {
            OnDamage?.Invoke(damage);
        }

        protected override void HandleDeadEvent()
        {
            Destroy(gameObject);
        }

        protected override void HandleHitEvent()
        {
        }
    }
}
