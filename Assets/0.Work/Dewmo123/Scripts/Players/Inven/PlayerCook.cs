using Agama.Scripts.Entities;
using Agama.Scripts.Events;
using Scripts.EventChannel;
using Scripts.Items;
using Scripts.UI.Inven;
using System;
using System.Collections;
using UnityEngine;

namespace Scripts.Players.Inven
{
    public class PlayerCook : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private EventChannelSO _invenChannel;
        private PlayerInvenData _invenCompo;
        public void Initialize(Entity owner)
        {
            _invenCompo = owner.GetComp<PlayerInvenData>();
            _invenChannel.AddListener<RequestCook>(HandleReqCook);
        }
        private void OnDestroy()
        {
            _invenChannel.RemoveListener<RequestCook>(HandleReqCook);
        }
        private void HandleReqCook(RequestCook cook)
        {
            if (_invenCompo.selectedItem.data is ConsumptionItemDataSO data && data.result != null)
            {
                _invenCompo.RemoveItem(data, 1);
                _invenCompo.AddItem(data.result);
            }
        }
    }
}