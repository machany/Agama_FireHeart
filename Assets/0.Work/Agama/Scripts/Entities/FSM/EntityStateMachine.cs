using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

namespace Agama.Scripts.Entities.FSM
{
    public class EntityStateMachine
    {
        public EntityState CurrentState { get; private set; }

        private Dictionary<string, EntityState> _states;
        private Queue<IEventState> _eventStates;

        private EntityState _defaultState;
        private int _maxEventStateStorageCount;

        public EntityStateMachine(Entity owner, EntityStateSOList stateList, int maxEventStateStorageCount = 0)
        {
            _states = new Dictionary<string, EntityState>();
            _eventStates = new Queue<IEventState>();
            _maxEventStateStorageCount = maxEventStateStorageCount;

            foreach (EntityStateSO state in stateList.entityStates)
            {
                Type stateType = Type.GetType(state.className);
                Debug.Assert(stateType != null, $"{owner.name} : can't instantiate class : {state.stateName}");
                EntityState entityState = Activator.CreateInstance(stateType, owner, state.animParam) as EntityState;
                _states.Add(state.stateName, entityState);
            }

            CurrentState = _defaultState = _states.GetValueOrDefault(stateList.defaultState.stateName);
            Debug.Assert(_defaultState != default(EntityState), $"{owner.name} : default state is null!");
            EnterState(CurrentState);
        }

        public void UpdateState()
        {
            CurrentState.Update();
        }

        #region State Change

        private void StateChange(EntityState newState)
        {
            ExitState(CurrentState);
            EnterState(CurrentState = newState);
        }

        private void EnterState(EntityState state)
        {
            state.Enter();

            IEventState newEventState = state as IEventState;
            if (newEventState != null)
            {
                newEventState.OnEventEndEvent += HandleEventEndEvent;
            }
        }

        private void ExitState(EntityState state)
        {
            state.Exit();

            IEventState previousEventState = state as IEventState;
            if (previousEventState != null)
            {
                previousEventState.OnEventEndEvent -= HandleEventEndEvent;
            }
        }

        public void ChangeState(string stateName)
        {
            EntityState newState = _states.GetValueOrDefault(stateName);
            Debug.Assert(newState != null, "could not find state or state is not event state.");

            if (newState is IEventState)
            {
                if (_eventStates.Count >= _maxEventStateStorageCount) // 이벤트 용량 제한. 이벤트 스테이트인데 용량 초과 시 무시
                    return;

                if (CurrentState is IEventState) // 현재 스테이트가 이벤트면 스테이트 체인지 (즉시 실행) 건너뜀
                {
                    _eventStates.Enqueue(newState as IEventState); // 그리고 이벤트에 추가 (이벤트를 대기상태로 만듬)
                    return; // 현재 상태가 이벤트 스테이트가 아니면 스테이트를 해당 스테이트로 변경 (즉시 변경하기에 저장할 필요 X)
                }
            } // 이벤트 스테이트가 아니면 스테이트를 해당 스테이트로 변경

            StateChange(newState);
        }

        #endregion

        private void HandleEventEndEvent()
            => StateChange(_eventStates.Count > 0 ? _eventStates.Dequeue() as EntityState : _defaultState);

        public void ClearEventState()
            => _eventStates.Clear();

        public void DestoryObject() // <- 생각좀 해봐야할듯
        {
            IEventState eventState = CurrentState as IEventState;
            if (eventState != null)
                eventState.OnEventEndEvent -= HandleEventEndEvent;
        }
    }
}
