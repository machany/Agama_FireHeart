using Agama.Scripts.Events;
using Scripts.EventChannel;
using UnityEngine;

namespace Scripts.UI.Utile
{
    public class ExitButton : MonoBehaviour
    {
        [SerializeField] private EventChannelSO _uiChannel;
        public void PressButton()
        {
            var evt = UIEvents.CloseEvent;
            _uiChannel.InvokeEvent(evt);
        }
    }
}
