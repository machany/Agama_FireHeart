using Agama.Scripts.Entities;

namespace Scripts.Stats
{
    public class EntityHealth : Stat
    {
        public override void Initialize(Entity entity)
        {
            base.Initialize(entity);
            _entity.OnDamage += ApplyDamage;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _entity.OnDamage -= ApplyDamage;
        }
    }
}
