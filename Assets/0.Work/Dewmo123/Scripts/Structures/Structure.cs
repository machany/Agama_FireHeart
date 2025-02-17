using Agama.Scripts.Combats;
using Agama.Scripts.Entities;
using Scripts.EventChannel;
using System;
using UnityEngine;

namespace Scripts.Structures
{
    public abstract class Structure : Entity, IDamageable
    {
        public event Action<int> OnDamage;
        public void ApplyDamage(int damage, bool isPowerAttack, Entity dealer)
        {
            OnDamage?.Invoke(damage);
        }
    }
}
