using Agama.Scripts.Entities;
using Scripts.Items;
using UnityEngine;

namespace Assets._0.Work.Dewmo123.Scripts.Items
{
    [CreateAssetMenu(fileName = "StructureItemDataSO", menuName = "SO/Items/Usable/StructureItem")]
    public class StructureItemDataSO : ItemDataSO, IUsable
    {
        public GameObject structure;
        public GameObject structureIcon => structure.transform.Find("Visual").gameObject;

        public void ChoiceItem(Entity entity)
        {
        }
        private void OnEnable()
        {
        }
        public void UseItem(Entity entity)
        {
        }

        protected override void Awake()
        {
            base.Awake();

            damageType *= -1;
        }
    }
}
