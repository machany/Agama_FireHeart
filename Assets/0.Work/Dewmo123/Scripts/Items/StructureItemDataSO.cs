using Agama.Scripts.Entities;
using Agama.Scripts.Players;
using Scripts.Items;
using Scripts.Map;
using Scripts.Players.Inven;
using UnityEngine;

namespace Assets._0.Work.Dewmo123.Scripts.Items
{
    [CreateAssetMenu(fileName = "StructureItemDataSO", menuName = "SO/Items/Usable/StructureItem")]
    public class StructureItemDataSO : ItemDataSO, IUsable
    {
        public GameObject structure;
        public GameObject structureIcon => structure.transform.GetChild(0).gameObject;

        public void ChoiceItem(Entity entity)
        {
        }
        private void OnEnable()
        {
        }
        public void UseItem(Entity entity)
        {
            var player = entity as Player;
            var input = player.InputSO;
            if (MapGenerator.Instance.BuildStructure((Vector2)player.transform.position + input.PreviousInputVector,structure))
            {
                var inven = player.GetComp<PlayerInvenData>();
                inven.RemoveItem(this, 1);
                inven.ReloadQuickSlot();
            }
        }

        protected override void Awake()
        {
            base.Awake();

            damageType *= -1;
        }
    }
}
