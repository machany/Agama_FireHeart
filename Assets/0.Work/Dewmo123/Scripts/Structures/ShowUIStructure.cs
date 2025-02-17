using Agama.Scripts.Events;
using Scripts.EventChannel;
using UnityEngine;

namespace Scripts.Structures
{
    public class ShowUIStructure : Structure,Interactable
    {
        [SerializeField] private EventChannelSO _uiChannel;
        public UIType myType;

        [ContextMenu("OpenPanel")]
        public void Interact()
        {
            var evt = UIEvents.OpenEvent;
            evt.type = myType;
            _uiChannel.InvokeEvent(evt);
        }
        protected override void HandleHitEvent()
        {
        }

        protected override void HandleDeadEvent()
        {
        }
    }
}
