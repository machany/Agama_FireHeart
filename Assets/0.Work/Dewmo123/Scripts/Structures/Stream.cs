using Agama.Scripts.Entities;
using Agama.Scripts.Events;
using Scripts.EventChannel;
using UnityEngine;

namespace Scripts.Structures
{
    public class Stream : MonoBehaviour, Interactable
    {
        [SerializeField] private EventChannelSO _utileChannel;
        [ContextMenu("Scoop")]
        public void Interact()
        {
            var evt = PlayerUtileEvents.ReqScoopEvent;
            _utileChannel.InvokeEvent(evt);
        }
    }
}
