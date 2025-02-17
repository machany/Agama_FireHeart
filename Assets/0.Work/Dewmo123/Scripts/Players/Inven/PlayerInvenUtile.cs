using Agama.Scripts.Entities;
using Agama.Scripts.Events;
using Scripts.EventChannel;
using Scripts.Items;
using System;
using UnityEngine;

namespace Scripts.Players.Inven
{
    public class PlayerInvenUtile : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private EventChannelSO _utileChannel;
        private PlayerInvenData _invenCompo;
        [Header("ScoopWater")]
        [SerializeField] private ItemDataSO _bucket; 
        [SerializeField] private ItemDataSO _waterBucket; 
        public void Initialize(Entity owner)
        {
            _invenCompo = owner.GetComp<PlayerInvenData>();
            _utileChannel.AddListener<RequestCook>(HandleReqCook);
            _utileChannel.AddListener<RequestScoopWater>(HandleScoopWater);
        }
        private void OnDestroy()
        {
            _utileChannel.RemoveListener<RequestScoopWater>(HandleScoopWater);
            _utileChannel.RemoveListener<RequestCook>(HandleReqCook);
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