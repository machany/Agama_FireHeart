using Agama.Scripts.Combats;
using Agama.Scripts.Entities;
using Scripts.Items;
using Unity.Behavior;
using UnityEngine;

namespace Agama.Scripts.Enemies
{
    public class TorchGoblin : BehaviorEnemy
    {
        public override void ApplyDamage(DamageMethodType damageType, float damage, Entity dealer)
        {
            if (damageType != DamageableType)
                damage = ItemDataSO.DEFAULT_DAMAGE;

            OnDamage?.Invoke(damage);
            Debug.Log("ssss");
        }

        protected override void HandleHitEvent()
        {
        }
    }
}