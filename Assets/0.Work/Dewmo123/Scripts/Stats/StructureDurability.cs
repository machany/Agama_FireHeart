using Agama.Scripts.Combats;
using Agama.Scripts.Core;
using Agama.Scripts.Entities;
using Scripts.Combat;
using UnityEngine;

namespace Scripts.Stats
{
    public class StructureDurability : Stat
    {
        [SerializeField]private StatBar _bar;
        public override void Initialize(Entity entity)
        {
            base.Initialize(entity);
            _entity.OnDamage += HandleHit;
        }

        private void HandleHit(float damage)
        {
            if (damage > 0)
                ApplyHeal(damage);
            else
                ApplyDamage(-damage);
        }
        public override void ApplyDamage(float damage)
        {
            //if (_entity.IsDead) return; //이미 죽은 녀석입니다.
            _bar.gameObject.SetActive(true);
            Debug.Log("Damaged");
            base.ApplyDamage(damage);
        }
        public override void ApplyHeal(float heal)
        {
            base.ApplyHeal(heal);
            if (currentStat.Value == maxStat)
                _bar.gameObject.SetActive(false);
        }
        public override void AfterHitFeedbacks()
        {
            _entity.OnHitEvent?.Invoke();
            base.AfterHitFeedbacks();
        }
    }
}
