using Agama.Scripts.Entities;

namespace Agama.Scripts.Combats
{
    public interface IDamageable
    {
        public void ApplyDamage(int damage, bool isPowerAttack, Entity dealer);
    }
}
