using Agama.Scripts.Core;
using Agama.Scripts.Entities;
using Agama.Scripts.Events;
using Agama.Scripts.Players;
using Scripts.EventChannel;
using Scripts.Items;
using System;
using UnityEngine;

namespace Scripts.Players.Inven
{
    public class PlayerInvenUtile : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private EventChannelSO _utileChannel;
        [SerializeField] private EventChannelSO _uiChannel;

        private PlayerInvenData _invenCompo;
        private PlayerInputSO _input;
        [Header("ScoopWater")]
        [SerializeField] private ItemDataSO _bucket; 
        [SerializeField] private ItemDataSO _waterBucket; 
        public void Initialize(Entity owner)
        {
            _input = (owner as Player).InputSO;
            _invenCompo = owner.GetComp<PlayerInvenData>();

            _input.OnInventoryKeyPressed += HandleInvenKey;
            _utileChannel.AddListener<RequestCook>(HandleReqCook);
            _utileChannel.AddListener<RequestScoopWater>(HandleScoopWater);
        }

        private void OnDestroy()
        {
            _input.OnInventoryKeyPressed -= HandleInvenKey;
            _utileChannel.RemoveListener<RequestScoopWater>(HandleScoopWater);
            _utileChannel.RemoveListener<RequestCook>(HandleReqCook);
        }
        private void HandleInvenKey()
        {
            var evt = UIEvents.OpenEvent;
            evt.type = UIType.Inventory;
            _uiChannel.InvokeEvent(evt);
        }

        private void HandleScoopWater(RequestScoopWater water)
        {
            if(_invenCompo.selectedItem.data == _bucket)
            {
                _invenCompo.RemoveItem(_bucket, 1);
                _invenCompo.AddItem(_waterBucket);
            }
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