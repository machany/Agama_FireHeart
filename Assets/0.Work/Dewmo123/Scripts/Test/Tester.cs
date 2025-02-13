using Dewmo123.Scripts.Items;
using UnityEngine;

namespace Dewmo123.Scripts.Test
{
    public class Tester : MonoBehaviour
    {
        [SerializeField] private ItemDropTableSO _tableSO;

        [ContextMenu("TableTest")]
        public void TestTable()
        {
            _tableSO.PullUpItem();
        }
    }
}
