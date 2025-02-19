using Agama.Scripts.Core;
using Agama.Scripts.Entities;
using Scripts.Combat;
using Scripts.Core;
using System;
using UnityEngine;

namespace Scripts.Structures
{
    public class EntityHealth : MonoBehaviour, IEntityComponent, IAfterInitialize
    {
        [SerializeField] private StatSO durabilityStat;
        public float maxHealth;

        public NotifyValue<float> currentDurability = new NotifyValue<float>();
        public float HealthPercent => currentDurability.Value / maxHealth;

        private Entity _entity;
        private EntityStat _statCompo;
        [SerializeField]private StatBar _bar;

        #region Initialize section

        public void Initialize(Entity entity)
        {
            _entity = entity;
        }

        public void AfterInitialize()
        {
            _statCompo = _entity.GetComp<EntityStat>();
            _statCompo.GetStat(durabilityStat).OnValueChange += HandleDurabilityChange;
            currentDurability.Value = maxHealth = _statCompo.GetStat(durabilityStat).Value;
            //_entity.OnDamage += ApplyDamage;
        }

        private void OnDestroy()
        {
            _statCompo.GetStat(durabilityStat).OnValueChange -= HandleDurabilityChange;
            //_entity.OnDamage -= ApplyDamage;
        }

        #endregion

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
                ApplyDamage(10, null);
        }
        private void HandleDurabilityChange(StatSO stat, float current, float previous)
        {
            maxHealth = current;
            currentDurability.Value = Mathf.Clamp(currentDurability.Value + current - previous, 1f, maxHealth);
            //체력변경으로 인해 사망하는 일은 없도록
        }

        public void ApplyDamage(float damage, Entity dealer)
        {
            //if (_entity.IsDead) return; //이미 죽은 녀석입니다.
            _bar.gameObject.SetActive(true);
            currentDurability.Value = Mathf.Clamp(currentDurability.Value - damage, 0, maxHealth);

            AfterHitFeedbacks();
        }
        public void ApplyHeal(float heal)
        {
            currentDurability.Value = Mathf.Clamp(currentDurability.Value + heal, 0, maxHealth);
            if (currentDurability.Value == maxHealth)
                _bar.gameObject.SetActive(false);
        }
        private void AfterHitFeedbacks()
        {
            _entity.OnHitEvent?.Invoke();

            if (currentDurability.Value <= 0)
            {
                _entity.OnDeadEvent?.Invoke();
            }
        }
    }
}
