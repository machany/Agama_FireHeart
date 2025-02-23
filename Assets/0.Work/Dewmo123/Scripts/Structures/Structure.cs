using Agama.Scripts.Combats;
using Agama.Scripts.Entities;
using Scripts.EventChannel;
using Scripts.Items;
using System;
using UnityEngine;

namespace Scripts.Structures
{
    public class Structure : Entity
    {
        protected override void HandleDeadEvent()
        {
            Destroy(gameObject);
        }

        protected override void HandleHitEvent()
        {
        }
        public override void ApplyDamage(DamageMethodType damageType, float damage, Entity dealer)
        {
            if (damageType != DamageMethodType.Harmmer)
            {
                if (damageType != DamageableType)
                    damage = ItemDataSO.DEFAULT_DAMAGE;
                damage *= -1;
            }
            OnDamage?.Invoke(damage);
        }
    }
}
