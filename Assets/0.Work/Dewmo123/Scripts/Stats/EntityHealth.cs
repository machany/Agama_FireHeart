using Agama.Scripts.Entities;
using System;
using UnityEngine;

namespace Scripts.Stats
{
    public class EntityHealth : Stat
    {

        public override void AfterHitFeedbacks()
        {
            _entity.OnHitEvent?.Invoke();
            base.AfterHitFeedbacks();
        }
        public override void Initialize(Entity entity)
        {
            base.Initialize(entity);
            _entity.OnResetEvent.AddListener(ResetHandler);
            _entity.OnDamage += ApplyDamage;
        }


        protected override void OnDestroy()
        {
            base.OnDestroy();
            _entity.OnResetEvent.RemoveListener(ResetHandler);
            _entity.OnDamage -= ApplyDamage;
        }
        private void ResetHandler()
        {
            ApplyHeal(maxStat);
        }
    }
}
