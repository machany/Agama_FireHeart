using Agama.Scripts.Entities;
using Scripts.Items;
using UnityEngine;

namespace Assets._0.Work.Dewmo123.Scripts.Items
{
    [CreateAssetMenu(fileName = "ToolItemDataSO", menuName = "SO/Items/Usable/ToolItem")]

    public class ToolItemDataSO : ItemDataSO, IUsable
    {
        [SerializeField] private float _attackDamageMultiply;
        public void ChoiceItem(Entity entity)
        {

        }

        public void UseItem(Entity entity)
        {
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            attackDamage = DEFAULT_DAMAGE * _attackDamageMultiply;
        }
    }
}
