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
    public class PlayerDeadState : EntityState, IEventState
    {
        public PlayerDeadState(Entity owner, AnimationParamiterSO animationParamitor) : base(owner, animationParamitor)
        {
        }

        public Action OnEventEndEvent { get; set; }

        public override void Enter()
        {
            base.Enter();
            _owner.GetComp<EntityMover>().CanMove = false;
        }
    }
}
