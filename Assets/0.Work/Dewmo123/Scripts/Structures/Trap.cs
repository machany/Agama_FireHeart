using Agama.Scripts.Combats;
using Agama.Scripts.Entities;
using Agama.Scripts.Players;
using UnityEngine;

namespace Scripts.Structures
{
    public class Trap : Structure
    {
        [Range(0,1)]
        [SerializeField] private float reflectPercent;

        public override void ApplyDamage(DamageMethodType damageType, float damage, Entity dealer)
        {
            if (dealer is not Player)
                dealer.ApplyDamage(dealer.DamageableType, damage * reflectPercent, this);
            base.ApplyDamage(damageType, damage, dealer);
        }
    }
}
