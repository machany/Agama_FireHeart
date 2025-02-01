using System;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

namespace Agama.Scripts.Events
{
    [CreateAssetMenu(fileName = "EventChannel", menuName = "SO/Event/Channel", order = 0)]
    public class EventChannelSO : ScriptableObject
    {
        private Dictionary<Delegate, Action<GameEvent>> _eventDoubleCastLocker = new Dictionary<Delegate, Action<GameEvent>>();
        private Dictionary<Type, Action<GameEvent>> _eventDictionary = new Dictionary<Type, Action<GameEvent>>();

        public void AddListener<T>(Action<T> listener) where T : GameEvent
        {
            if (_eventDoubleCastLocker.ContainsKey(listener))
                return;

            Action<GameEvent> changer = gameEvent => listener.Invoke(gameEvent as T);
            _eventDoubleCastLocker[listener] = changer;

            Type eventType = typeof(T);
            if (_eventDictionary.ContainsKey(eventType))
                _eventDictionary[eventType] += changer;
            else
                _eventDictionary[eventType] = changer;
        }

        public void RemoveListener<T>(Action<T> listener) where T : GameEvent
        {
            if (_eventDoubleCastLocker.TryGetValue(listener, out Action<GameEvent> changer))
            {
                Type eventType = typeof(T);
                if (_eventDictionary.TryGetValue(eventType, out Action<GameEvent> invoker))
                {
                    if ((invoker -= changer) == null)
                        _eventDictionary.Remove(eventType);
                    else
                        _eventDictionary[eventType] = invoker;
                }
                _eventDoubleCastLocker.Remove(listener);
            }
        }

        public void InvokeEvent(GameEvent gameEvent)
        {
            if (_eventDictionary.TryGetValue(gameEvent.GetType(), out Action<GameEvent> invoker))
                invoker?.Invoke(gameEvent);
        }

        public void ClearAllEvent()
        {
            _eventDictionary.Clear();
            _eventDoubleCastLocker.Clear();
        }
    }

    public abstract class GameEvent { }
}
