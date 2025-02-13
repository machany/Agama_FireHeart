using Agama.Scripts.Events;
using Dewmo123.Scripts.Items;
using Scripts.EventChannel;
using Scripts.Utiles;
using UnityEngine;

namespace Scripts.Feedbacks
{
    public class DropFeedback : Feedback
    {
        [SerializeField] private ItemDropTableSO _table;
        [SerializeField] private EventChannelSO _invenChannel;
        [SerializeField] private GameObject _dropTxt;//우리 pool 필요할듯
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
                ItemDropText txt = Instantiate(_dropTxt, spawnPos, Quaternion.identity).GetComponent<ItemDropText>();
                spawnPos.y += _interval;
                txt.Init($"{item.Key.itemName}: {item.Value}개 획득");
            }
        }
    }
}
