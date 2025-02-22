using Agama.Scripts.Combats;
using Agama.Scripts.Entities;
using Dewmo123.Scripts.Items;
using UnityEngine;

namespace Dewmo123.Scripts.Map
{
    public class ResourceEntity : Entity,IDamageable
    {
        [SerializeField] private DamageMethodType _damageType;
        public DamageMethodType DamageableType => _damageType;

        public void ApplyDamage(DamageMethodType damageType, float damage, Entity dealer)
        {
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
