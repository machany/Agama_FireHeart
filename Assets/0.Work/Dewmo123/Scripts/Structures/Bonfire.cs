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
            throw new System.NotImplementedException();
        }

        protected override void HandleHitEvent()
        {
            throw new System.NotImplementedException();
        }
    }
}
