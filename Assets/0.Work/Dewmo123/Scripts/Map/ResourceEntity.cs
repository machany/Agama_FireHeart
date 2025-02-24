using Agama.Scripts.Combats;
using Agama.Scripts.Entities;
using Dewmo123.Scripts.Items;
using Scripts.Items;
using Scripts.Map;
using UnityEngine;

namespace Dewmo123.Scripts.Map
{
    public class ResourceEntity : Entity
    {
        protected override void HandleDeadEvent()
        {
            MapGenerator.Instance.DestoryStructure(transform.position);
            Destroy(gameObject);
        }

        protected override void HandleHitEvent()
        {
        }
        public override void ApplyDamage(DamageMethodType damageType, float damage, Entity dealer)
        {
            if (damageType != DamageableType)
                damage = ItemDataSO.DEFAULT_DAMAGE;
            damage *= -1;
            base.ApplyDamage(damageType, damage, dealer);
        }
    }
}
