using Agama.Scripts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agama.Scripts.Players
{
    public class PlayerAttackComponent : EntityAttackComponent
    {
        public override void Initialize(Entity owner)
        {
            base.Initialize(owner);

            damagecaster.InitCaster(owner);
        }

        public override void Attack()
        {
            base.Attack();

            damagecaster.CastDamage(1, true);
        }

        private void Update()
        {
            damagecaster.UpdateCaster();
        }
    }
}
