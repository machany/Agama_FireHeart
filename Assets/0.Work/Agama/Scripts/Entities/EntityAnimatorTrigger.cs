using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Agama.Scripts.Entities
{
    public class EntityAnimatorTrigger : MonoBehaviour, IEntityComponent
    {
        private Entity _owner;

        public void Initialize(Entity owner)
        {
            _owner = owner;
        }

        public event Action OnAnimationEndEvent;
        public event Action OnAnimationEvent;

        private void OnAnimationEndTrigger()
            => OnAnimationEndEvent?.Invoke();

        private void OnAnimationEventTrigger()
            => OnAnimationEvent?.Invoke();
    }
}
