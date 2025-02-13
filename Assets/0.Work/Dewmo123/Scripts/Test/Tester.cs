using Agama.Scripts.Entities;
using Dewmo123.Scripts.Items;
using UnityEngine;
using UnityEngine.Events;

namespace Dewmo123.Scripts.Test
{
    public class Tester : MonoBehaviour
    {
        public UnityEvent OnDeadEvent;
        [SerializeField] private ItemDropTableSO _tableSO;

        [ContextMenu("TableTest")]
        public void TestTable()
        {
            _tableSO.PullUpItem();
        }

        [ContextMenu("DeadEvent")]
        public void Dead()
        {
            OnDeadEvent?.Invoke();
        }
    }
}
