using Agama.Scripts.Combats.DamageCasters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Agama.Scripts.Entities
{
    public abstract class EntityAttackComponent : MonoBehaviour, IEntityComponent
    {
        [SerializeField] protected DamageCaster damagecaster;

        public virtual void Initialize(Entity owner)
        {

        }

        public virtual void Attack()
        {

        }
    }
}
