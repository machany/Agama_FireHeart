using Agama.Scripts.Events;
using Scripts.EventChannel;
using UnityEngine;

namespace Scripts.Structures
{
    public class Bonfire : Structure, IInteracterable
    {
        [SerializeField] private EventChannelSO _invenChannel;
        [ContextMenu("Cook")]
        public void Interact()
        {
            var evt = PlayerUtileEvents.ReqCookEvent;
            _invenChannel.InvokeEvent(evt);
        }

        protected override void HandleDeadEvent()
        {
        }

        protected override void HandleHitEvent()
        {
        }
    }
}
