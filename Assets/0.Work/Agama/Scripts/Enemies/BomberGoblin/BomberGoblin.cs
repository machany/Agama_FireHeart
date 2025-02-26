using Agama.Scripts.Combats;
using Agama.Scripts.Entities;
using Scripts.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Agama.Scripts.Enemies.Mosters
{
    public class BomberGoblin : BehaviorEnemy
    {
        public override void ApplyDamage(DamageMethodType damageType, float damage, Entity dealer)
        {
            if (damageType != DamageableType)
                damage = ItemDataSO.DEFAULT_DAMAGE;

            OnDamage?.Invoke(damage);
        }
    }
}
