using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Agama.Scripts.Entities
{
    public abstract class Entity : MonoBehaviour
    {
        public UnityEvent OnHitEvent;
        public UnityEvent OnDeadEvent;

        protected Dictionary<Type, IEntityComponent> _entityComponentDictionary;

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
            transform.GetComponentsInChildren<IEntityComponent>().ToList().ForEach(component =>
            {
                _entityComponentDictionary.Add(component.GetType(), component);
            });
        }
        protected virtual void Init()
        {
            _entityComponentDictionary.ToList().ForEach(component =>
            {
                component.Value.Initialize(this);
            });
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
    }
}