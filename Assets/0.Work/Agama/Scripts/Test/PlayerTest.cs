using Agama.Scripts.Animators;
using Agama.Scripts.Core;
using Agama.Scripts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Agama.Scripts.Test
{
    public class PlayerTest : MonoBehaviour, IEntityComponent
    {
        [Header("Setting")]
        [SerializeField] private PlayerInputSO playerInputSO;
        [SerializeField] private AnimationParamiterSO carryParam;

        [Header("Values")]
        [SerializeField] private sbyte toolTypeValue;
        [SerializeField] private bool carry;

        [ContextMenu("Tool Type Change")]
        private void ToolTypeChangeEventInvoke()
        {
            playerInputSO.OnQuickSlotChangedEvent?.Invoke(toolTypeValue);
        }

        [ContextMenu("Carry ValueChanged")]
        private void CarryValueChanged()
        {
            _owner.GetComp<EntityRenderer>().SetParamiter(carryParam, carry);
        }

        private Entity _owner;

        public void Initialize(Entity owner)
        {
            _owner = owner;
        }
    }
}
