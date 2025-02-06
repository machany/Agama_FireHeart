using Agama.Scripts.Events;
using Scripts.EventChannel;
using UnityEngine;

namespace Scripts.Structures
{
    public class InteractableStructure : Structure
    {
        [SerializeField] private EventChannelSO _uiChannel;
        [ContextMenu("OpenPanel")]
        public override void Activate()
        {
            var evt = UIEvents.OpenEvent;
            evt.type = myType;
            _uiChannel.InvokeEvent(evt);
        }
    }
}
