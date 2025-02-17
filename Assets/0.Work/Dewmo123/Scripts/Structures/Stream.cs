using Agama.Scripts.Entities;
using Agama.Scripts.Events;
using Scripts.EventChannel;
using UnityEngine;

namespace Scripts.Structures
{
    public class Stream : Structure, IInteracterable
    {
        [SerializeField] private EventChannelSO _utileChannel;
        [ContextMenu("Scoop")]
        public void Interact()
        {
            var evt = PlayerUtileEvents.ReqScoopEvent;
            _utileChannel.InvokeEvent(evt);
        }

        protected override void HandleDeadEvent()
        {
        }

        protected override void HandleHitEvent()
        {
        }
    }
}
