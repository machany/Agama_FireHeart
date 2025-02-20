using Agama.Scripts.Core;
using Agama.Scripts.Entities;
using Scripts.Combat;
using Scripts.Core;
using UnityEngine;

namespace Scripts.Stats
{
    public class Stat : MonoBehaviour, IEntityComponent, IAfterInitialize
    {
        [SerializeField] protected StatSO _stat;
        public float maxStat;

        public NotifyValue<float> currentStat = new NotifyValue<float>();
        public float StatPercent => currentStat.Value / maxStat;

        protected Entity _entity;
        protected EntityStat _statCompo;

        #region Initialize section

        public virtual void Initialize(Entity entity)
        {
            _entity = entity;
            _statCompo = _entity.GetComp<EntityStat>();
            //_entity.OnDamage += ApplyDamage;
        }
        public virtual void AfterInitialize()
        {
            _statCompo.GetStat(_stat).OnValueChange += HandleStatChange;
            currentStat.Value = maxStat = _statCompo.GetStat(_stat).Value;
        }

        public virtual void OnDestroy()
        {
            _statCompo.GetStat(_stat).OnValueChange -= HandleStatChange;
            //_entity.OnDamage -= ApplyDamage;
        }

        #endregion

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
                ApplyDamage(10);
        }
        public virtual void HandleStatChange(StatSO stat, float current, float previous)
        {
            maxStat = current;
            currentStat.Value = Mathf.Clamp(currentStat.Value + current - previous, 1f, maxStat);
            //체력변경으로 인해 사망하는 일은 없도록
        }

        public virtual void ApplyDamage(float damage)
        {
            //if (_entity.IsDead) return; //이미 죽은 녀석입니다.
            currentStat.Value = Mathf.Clamp(currentStat.Value - damage, 0, maxStat);

            AfterHitFeedbacks();
        }
        public virtual void ApplyHeal(float heal)
        {
            currentStat.Value = Mathf.Clamp(currentStat.Value + heal, 0, maxStat);
        }
        public virtual void AfterHitFeedbacks()
        {
            _entity.OnHitEvent?.Invoke();

            if (currentStat.Value <= 0)
            {
                _entity.OnDeadEvent?.Invoke();
            }
        }

    }
}
