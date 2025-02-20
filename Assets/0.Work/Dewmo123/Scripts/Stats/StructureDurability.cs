using Agama.Scripts.Core;
using Agama.Scripts.Entities;
using Scripts.Combat;
using UnityEngine;

namespace Scripts.Stats
{
    public class StructureDurability : Stat
    {
        [SerializeField]private StatBar _bar;

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
    }
}
