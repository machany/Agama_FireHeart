using Agama.Scripts.Combats;
using Agama.Scripts.Core;
using Agama.Scripts.Entities;
using Scripts.Items;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._0.Work.Dewmo123.Scripts.Items
{
    [CreateAssetMenu(fileName = "ToolItemDataSO", menuName = "SO/Items/Usable/ToolItem")]

    public class ToolItemDataSO : ItemDataSO, IUsable
    {
        public IDamageable.DamageMethodType damageType;
        public List<AddingStat> addingStats;
        public void AddModifier(EntityStat statCompo)
        {
            foreach (AddingStat stat in addingStats)
            {
                StatSO targetStat = statCompo.GetStat(stat.targetStat);
                if (targetStat != null)
                {
                    targetStat.AddModifier(itemName, stat.modifyValue);
                }
            }
        }


        public void ChoiceItem(Entity entity)
        {
            AddModifier(entity.GetComp<EntityStat>());
        }

        public void UseItem(Entity entity)
        {
        }
    }
}
