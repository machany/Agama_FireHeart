using Agama.Scripts.Events;
using Scripts.EventChannel;
using Scripts.Structures;
using UnityEngine;

namespace Assets._0.Work.Dewmo123.Scripts.Structures
{
    public class Tent : Structure, IInteractable
    {
        [SerializeField] private EventChannelSO _utileChannel;
        [ContextMenu("Select")]
        public void Interact()
        {
            var evt = PlayerUtileEvents.SelectTentEvent;
            evt.tent = this;
            _utileChannel.InvokeEvent(evt);
        }
    }
}
