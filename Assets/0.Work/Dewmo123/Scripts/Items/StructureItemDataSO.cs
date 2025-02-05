using Agama.Scripts.Entities;
using Scripts.Items;
using UnityEngine;

namespace Assets._0.Work.Dewmo123.Scripts.Items
{
    [CreateAssetMenu(fileName = "StructureItemDataSO", menuName = "SO/Items/Usable/StructureItem")]
    public class StructureItemDataSO : ItemDataSO, IUsable
    {

        public void UseItem(Entity entity)
        {
        }
    }
}
