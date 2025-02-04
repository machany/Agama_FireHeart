using Agama.Scripts.Animators;
using Agama.Scripts.Entities;
using Agama.Scripts.Entities.FSM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agama.Scripts.Players.States
{
    public class PlayerUseToolsState : EntityState, IEventState
    {
        public Action OnEventEndEvent { get; set; }

        public PlayerUseToolsState(Entity owner, AnimationParamitorSO animationParamitor) : base(owner, animationParamitor)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _owner.GetComp<EntityAnimatorTrigger>().OnAnimationEndEvent += HaandleAnimationEndEvent;
        }

        private void HaandleAnimationEndEvent()
        {
            _owner.GetComp<EntityAnimatorTrigger>().OnAnimationEndEvent -= HaandleAnimationEndEvent;
            OnEventEndEvent?.Invoke();
        }
    }
}
