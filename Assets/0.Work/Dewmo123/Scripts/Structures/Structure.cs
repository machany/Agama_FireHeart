using Agama.Scripts.Combats;
using Agama.Scripts.Entities;
using Scripts.EventChannel;
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
                damage *= -1;
            base.ApplyDamage(damageType, damage, dealer);
        }
    }
}
