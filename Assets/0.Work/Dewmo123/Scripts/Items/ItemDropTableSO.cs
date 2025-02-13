using Scripts.Items;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dewmo123.Scripts.Items
{
    [Serializable]
    public struct DropInfo
    {
        public int minCnt, maxCnt;
        [Range(0,1)]
        public float probability;
        public ItemDataSO item;
    }
    [CreateAssetMenu(fileName ="ItemDropTableSO",menuName ="SO/Items/DropTable")]
    public class ItemDropTableSO : ScriptableObject
    {
        public List<DropInfo> table;
        public Dictionary<ItemDataSO,int> PullUpItem()
        {
            Dictionary<ItemDataSO, int> info = new Dictionary<ItemDataSO, int>();
            foreach(var item in table)
            {
                info.Add(item.item, item.minCnt);
                for(int i = 0; i < item.maxCnt - item.minCnt; i++)
                    if (UnityEngine.Random.value < item.probability)
                        info[item.item]++;
            }
            foreach (var item in info)
                if (item.Value <= 0)
                    info.Remove(item.Key);
            return info;
        }
    }
}
