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
            transform.GetComponentsInChildren<IEntityComponent>().ToList().ForEach(component => {
                _entityComponentDictionary.Add(component.GetType(), component);
                component.Initialize(this);
            });
            _entityComponentDictionary.Values.OfType<IAfterInitialize>().ToList().ForEach(component => component.AfterInitialize());
            OnHitEvent.AddListener(HandleHitEvent);
            OnHitEvent.AddListener(HandleHitEvent);
        }

        protected abstract void HandleHitEvent();

        protected virtual void OnDestroy()
        {

        }
    }
}