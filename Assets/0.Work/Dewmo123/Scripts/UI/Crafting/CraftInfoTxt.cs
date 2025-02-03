using Agama.Scripts.Events;
using Scripts.EventChannel;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts.UI.Crafting
{
    public class CraftInfoTxt : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _title, _description;
        [SerializeField] private EventChannelSO _craftingChannel;

        private void OnEnable()
        {
            _craftingChannel.AddListener<CraftInfo>(HandleSelectChange);
        }
        private void OnDisable()
        {
            _craftingChannel.RemoveListener<CraftInfo>(HandleSelectChange);
        }
        public void HandleSelectChange(CraftInfo evt)
        {
            _title.text = evt.title;
            _description.text = evt.description;
        }

    }
}
