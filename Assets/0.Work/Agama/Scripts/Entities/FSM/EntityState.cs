using Agama.Scripts.Animators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agama.Scripts.Entities.FSM
{
    public class EntityState
    {
        protected AnimationParamiterSO _animParam;

        protected Entity _owner;
        protected EntityRenderer _renderer;

        public EntityState(Entity owner, AnimationParamiterSO animationParamitor)
        {
            _owner = owner;
            _animParam = animationParamitor;
            _renderer = _owner.GetComp<EntityRenderer>(true);
        }

        public virtual void Enter()
        {
            _renderer.SetParamitor(_animParam, true);
        }

        public virtual void Update() { }

        public virtual void Exit()
        {
            _renderer.SetParamitor(_animParam, false);
        }
    }
}
