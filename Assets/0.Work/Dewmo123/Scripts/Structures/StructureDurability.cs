using Agama.Scripts.Core;
using Agama.Scripts.Entities;
using System;
using UnityEngine;

namespace Scripts.Structures
{
    public class StructureDurability : MonoBehaviour, IEntityComponent, IAfterInitialize
    {
        [SerializeField] private StatSO durabilityStat;
        public float maxHealth;
        private float _currentHealth;

        private Entity _entity;
        private EntityStat _statCompo;

        #region Initialize section

        public void Initialize(Entity entity)
        {
            _entity = entity;
            _statCompo = _entity.GetComp<EntityStat>();
        }

        public void AfterInitialize()
        {
            _statCompo.GetStat(durabilityStat).OnValueChange += HandleDurabilityChange;
            _currentHealth = maxHealth = _statCompo.GetStat(durabilityStat).Value;
            //_entity.OnDamage += ApplyDamage;
        }

        private void OnDestroy()
        {
            _statCompo.GetStat(durabilityStat).OnValueChange -= HandleDurabilityChange;
            //_entity.OnDamage -= ApplyDamage;
        }

        #endregion


        private void HandleDurabilityChange(StatSO stat, float current, float previous)
        {
            maxHealth = current;
            _currentHealth = Mathf.Clamp(_currentHealth + current - previous, 1f, maxHealth);
            //체력변경으로 인해 사망하는 일은 없도록
        }

        public void ApplyDamage(float damage, Vector2 direction, Vector2 knockBackPower, bool isPowerAttack, Entity dealer)
        {
            //if (_entity.IsDead) return; //이미 죽은 녀석입니다.

            _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, maxHealth);


            AfterHitFeedbacks(knockBackPower);
        }
        public void ApplyHeal(float heal)
        {

        }
        private void AfterHitFeedbacks(Vector2 knockBackPower)
        {
            _entity.OnHitEvent?.Invoke();

            if (_currentHealth <= 0)
            {
                _entity.OnDeadEvent?.Invoke();
            }
        }
    }
}
