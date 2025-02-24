using Agama.Scripts.Combats;
using Agama.Scripts.Entities;
using Scripts.Items;
using Unity.Behavior;

namespace Agama.Scripts.Enemies
{
    public class Animal : BehaviorEnemy
    {
        [BlackboardEnum]
        public enum AniamlState
        {
            Patroll,
            Run,
            Hit,
            Dead
        }

        protected override void HandleHitEvent()
        {
        }

        public override void ApplyDamage(DamageMethodType damageType, float damage, Entity dealer)
        {
            if (damageType != DamageableType)
                damage = ItemDataSO.DEFAULT_DAMAGE;
            OnDamage?.Invoke(damage);
        }
    }
}
