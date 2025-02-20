using Agama.Scripts.Entities;
using Scripts.Players.Inven;
using Scripts.Stats;
using SerializableDictionary.Scripts;
using UnityEngine;

namespace Scripts.Items
{
    [CreateAssetMenu(fileName = "ConsumptionItemDataSO", menuName = "SO/Items/Usable/ConsumptionItem")]
    public class ConsumptionItemDataSO : ItemDataSO, IUsable
    {
        //hp 회복량, 갈증 회복량 등 인벤 먼저하고 구현
        public ConsumptionItemDataSO result;
        public float hp, thirsty, hungry;

        public void ChoiceItem(Entity entity)
        {

        }

        public void UseItem(Entity entity)
        {
            entity.GetComp<PlayerInvenData>().RemoveItem(this, 1);
            entity.GetComp<EntityHealth>().ApplyHeal(hp);
            entity.GetComp<EntityHealth>().ApplyHeal(thirsty);
            entity.GetComp<EntityHealth>().ApplyHeal(hungry);
        }
    }
}
