using Agama.Scripts.Combats;
using Agama.Scripts.Entities;
using Dewmo123.Scripts.Items;
using UnityEngine;

namespace Dewmo123.Scripts.Map
{
    public class ResourceEntity : Entity
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
            damage *= -1;
            base.ApplyDamage(damageType, damage, dealer);
        }
    }
}
