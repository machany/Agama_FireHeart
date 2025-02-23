using Agama.Scripts.Combats;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Agama.Scripts.Entities
{
    public abstract class Entity : MonoBehaviour,IDamageable
    {
        public UnityEvent OnHitEvent;
        public UnityEvent OnDeadEvent;
        public Action<float> OnDamage;

        protected Dictionary<Type, IEntityComponent> _entityComponentDictionary;
        [SerializeField] private DamageMethodType _damageType;
        public DamageMethodType DamageableType => _damageType;

        protected virtual void Awake()
        {
            _entityComponentDictionary = new Dictionary<Type, IEntityComponent>();
            Initialize();
            Init();
            AfterInitialize();

            OnHitEvent.AddListener(HandleHitEvent);
            OnDeadEvent.AddListener(HandleDeadEvent);
        }

        protected virtual void OnDestroy()
        {
            OnHitEvent.RemoveListener(HandleHitEvent);
            OnDeadEvent.RemoveListener(HandleDeadEvent);
        }

        protected virtual void Initialize()
        {
            transform.GetComponentsInChildren<IEntityComponent>().ToList().ForEach(component => _entityComponentDictionary.Add(component.GetType(), component));
        }

        protected virtual void Init()
        {
            _entityComponentDictionary.Values.ToList().ForEach(component => component.Initialize(this));
        }

        protected virtual void AfterInitialize()
        {
            _entityComponentDictionary.Values.OfType<IAfterInitialize>().ToList().ForEach(component => component.AfterInitialize());
        }

        protected abstract void HandleHitEvent();
        protected abstract void HandleDeadEvent();

        public T GetComp<T>(bool isDerived = false) where T : IEntityComponent
        {
            if (_entityComponentDictionary.TryGetValue(typeof(T), out IEntityComponent component))
                return (T)component;

            if (!isDerived) return default(T);

            Type findType = _entityComponentDictionary.Keys.FirstOrDefault(type => type.IsSubclassOf(typeof(T)));
            if (findType != null)
                return (T)_entityComponentDictionary[findType];

            return default(T);
        }

        public virtual void ApplyDamage(DamageMethodType damageType, float damage, Entity dealer)
        {
            //Entity 관련 처리는 그냥 여기서
            OnDamage?.Invoke(damage);
        }
    }
}