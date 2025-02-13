using Agama.Scripts.Events;
using Dewmo123.Scripts.Items;
using GGMPool;
using Scripts.EventChannel;
using Scripts.Utiles;
using UnityEngine;

namespace Scripts.Feedbacks
{
    public class DropFeedback : Feedback
    {
        [SerializeField] private ItemDropTableSO _table;
        [SerializeField] private EventChannelSO _invenChannel;
        [SerializeField] private PoolTypeSO _dropTxt;//우리 pool 필요할듯
        [SerializeField] private PoolManagerSO _poolManager;
        [SerializeField] private float _interval;
        public override void CreateFeedback()
        {
            Vector3 spawnPos = transform.position;
            foreach (var item in _table.PullUpItem())
            {
                var evt = InvenEvents.AddItemEvent;
                evt.item = item.Key;
                evt.cnt = item.Value;
                _invenChannel.InvokeEvent(evt);
                var txt = _poolManager.Pop(_dropTxt) as ItemDropText;
                txt.Init($"{item.Key.itemName}: {item.Value}개 획득",spawnPos);
                spawnPos.y += _interval;
            }
        }
    }
}
