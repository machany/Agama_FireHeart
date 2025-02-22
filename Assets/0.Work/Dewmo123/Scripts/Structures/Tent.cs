using Agama.Scripts.Events;
using Scripts.EventChannel;
using Scripts.Structures;
using UnityEngine;

namespace Assets._0.Work.Dewmo123.Scripts.Structures
{
    public class Tent : Structure, Interactable
    {
        [SerializeField] private EventChannelSO _utileChannel;
        public void Interact()
        {
            var evt = PlayerUtileEvents.SelectTentEvent;
            _utileChannel.InvokeEvent(evt);
        }
        protected override void HandleDeadEvent()
        {
            base.HandleDeadEvent();
        }
    }
}
