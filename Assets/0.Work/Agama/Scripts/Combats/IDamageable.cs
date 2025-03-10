﻿using Agama.Scripts.Entities;

namespace Agama.Scripts.Combats
{
    public enum DamageMethodType
    {
        Entity = 4,
        Chop = 1,
        Harmmer = 2,
        Pickax = 3,
    }

    public interface IDamageable
    {
        public DamageMethodType DamageableType { get; }

        public void ApplyDamage(DamageMethodType damageType, float damage, Entity dealer);
    }
}
