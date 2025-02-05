using Agama.Scripts.Animators;
using Agama.Scripts.Entities;
using Agama.Scripts.Entities.FSM;
using System;
using UnityEngine;

namespace Agama.Scripts.Test
{
    public class TestEventState : EntityState, IEventState
    {
        public TestEventState(Entity owner, AnimationParamitorSO animationParamitor) : base(owner, animationParamitor)
        {
        }

        public Action OnEventEndEvent { get; set; }
        private float _startTime;

        public override void Update()
        {
            base.Update();
            if (_startTime > 1)
            {
            _startTime = 0;
                OnEventEndEvent?.Invoke();
            }
            else
            {
                _startTime += Time.deltaTime;
            }
        }

        public void Excute()
        {
            Debug.Log("Enter : Event");
            _startTime = 0;
        }

        public void UnDo()
        {
            Debug.Log("Exit : Event");
            _startTime = 0;
        }
    }

    public class TestState : EntityState
    {
        public TestState(Entity owner, AnimationParamitorSO animationParamitor) : base(owner, animationParamitor)
        {
        }

        public override void Enter()
        {
            //Debug.Log("Enter : TestState");
        }

        public override void Exit()
        {
            //Debug.Log("Exit : TestState");
        }
    }

    public class TestDefaultState : EntityState
    {
        public TestDefaultState(Entity owner, AnimationParamitorSO animationParamitor) : base(owner, animationParamitor)
        {
        }

        public override void Enter()
        {
            //Debug.Log("Enter : Default");
        }

        public override void Exit()
        {
        }
    }
}
