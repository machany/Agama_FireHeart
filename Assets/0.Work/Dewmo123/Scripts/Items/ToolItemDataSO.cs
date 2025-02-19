using Agama.Scripts.Combats;
using Agama.Scripts.Entities;
using Scripts.Items;
using UnityEngine;

namespace Assets._0.Work.Dewmo123.Scripts.Items
{
    [CreateAssetMenu(fileName = "ToolItemDataSO", menuName = "SO/Items/Usable/ToolItem")]

    public class ToolItemDataSO : ItemDataSO, IUsable
    {
        public DamageMethodType damageType;

        public void ChoiceItem(Entity entity)
        {
        }

        public void UseItem(Entity entity)
        {
        }
    }
}
