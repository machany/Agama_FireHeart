using Agama.Scripts.Entities;
using SerializableDictionary.Scripts;
using UnityEngine;

namespace Scripts.Items
{
    [CreateAssetMenu(fileName = "ConsumptionItemDataSO", menuName = "SO/Items/Usable/ConsumptionItem")]
    public class ConsumptionItemDataSO : ItemDataSO, IUsable
    {
        //hp 회복량, 갈증 회복량 등 인벤 먼저하고 구현
        public void UseItem(Entity entity)
        {
        }
    }
}
