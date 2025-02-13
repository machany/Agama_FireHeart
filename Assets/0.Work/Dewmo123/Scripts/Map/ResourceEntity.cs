using Agama.Scripts.Entities;
using Dewmo123.Scripts.Items;
using UnityEngine;

namespace Dewmo123.Scripts.Map
{
    public class ResourceEntity : Entity
    {
        [SerializeField] private ItemDropTableSO _dropTable;
        protected override void HandleDeadEvent()
        {
        }

        protected override void HandleHitEvent()
        {
        }
    }
}
