using Scripts.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Dewmo123.Scripts.Items
{
    [Serializable]
    public struct DropInfo
    {
        public int minCnt, maxCnt;
        [Range(0, 1)]
        public float probability;
        public ItemDataSO item;
    }
    [CreateAssetMenu(fileName = "ItemDropTableSO", menuName = "SO/Items/DropTable")]
    public class ItemDropTableSO : ScriptableObject
    {
        public List<DropInfo> table;
        public List<KeyValuePair<ItemDataSO, int>> PullUpItem()
        {
            Dictionary<ItemDataSO, int> info = new Dictionary<ItemDataSO, int>();
            foreach (var item in table)
            {
                info.Add(item.item, item.minCnt);
                for (int i = 0; i < item.maxCnt - item.minCnt; i++)
                    if (UnityEngine.Random.value < item.probability)
                        info[item.item]++;
                    else
                        break;
            }
            var list = info.ToList();
            list.RemoveAll(item => item.Value <= 0);
            return list;
        }
    }
}
