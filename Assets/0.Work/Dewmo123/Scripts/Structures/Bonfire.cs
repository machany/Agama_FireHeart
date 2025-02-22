using Agama.Scripts.Events;
using Scripts.EventChannel;
using UnityEngine;

namespace Scripts.Structures
{
    public class Bonfire : Structure, IInteractable
    {
        [SerializeField] private EventChannelSO _utileChannel;
        [ContextMenu("Cook")]
        public void Interact()
        {
            var evt = PlayerUtileEvents.ReqCookEvent;
            _utileChannel.InvokeEvent(evt);
        }

    }
}
