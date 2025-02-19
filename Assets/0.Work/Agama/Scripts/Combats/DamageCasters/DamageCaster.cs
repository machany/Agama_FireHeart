using Agama.Scripts.Entities;
using UnityEngine;

namespace Agama.Scripts.Combats.DamageCasters
{
    public abstract class DamageCaster : MonoBehaviour
    {
        [SerializeField] protected int maxHitCount = 1;
        [SerializeField] protected ContactFilter2D contactFilter;

        protected Entity _owner;

        public virtual void InitCaster(Entity owner)
        {
            _owner = owner;
        }

        public virtual void UpdateCaster() { }

        public Transform TargetTrm => _owner.transform;
        public abstract bool CastDamage(int damage, bool isPowerAttack);

        protected IDamageable.DamageMethodType _currentDamageType = IDamageable.DamageMethodType.Entity;
        public void ChangeDamageType(IDamageable.DamageMethodType newDamageType)
            => _currentDamageType = newDamageType;
        public void ChangeDamageType(sbyte value)
            => _currentDamageType = value switch
            {
                1 => IDamageable.DamageMethodType.Chop,
                2 => IDamageable.DamageMethodType.Harmmer,
                3 => IDamageable.DamageMethodType.Pickax,
                _ => IDamageable.DamageMethodType.Entity
            };
    }
}
