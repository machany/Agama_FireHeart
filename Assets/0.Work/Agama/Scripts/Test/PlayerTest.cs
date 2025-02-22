using Agama.Scripts.Animators;
using Agama.Scripts.Combats;
using Agama.Scripts.Core;
using Agama.Scripts.Entities;
using Agama.Scripts.Players;
using UnityEngine;

namespace Agama.Scripts.Test
{
    public class PlayerTest : MonoBehaviour, IEntityComponent
    {
        [Header("Setting")]
        [SerializeField] private Player player;

        [Header("Values")]
        [SerializeField] private DamageMethodType toolTypeValue;
        [SerializeField] private float damage = 10;
        [SerializeField] private bool carry;

        private bool _start = false;

        [ContextMenu("Tool Type Change")]
        private void ToolTypeChangeEventInvoke()
        {
            player.ChangeQuickSlotItem((sbyte)(carry ? -1 : toolTypeValue switch
            {
                DamageMethodType.Chop => 1,
                DamageMethodType.Harmmer => 2,
                DamageMethodType.Pickax => 3,
                DamageMethodType.Entity => 4,
                _ => 0
            }), damage);
        }

        [ContextMenu("Invoke Hit Event")]
        private void InvokeHitEvent()
        {
            _owner.OnHitEvent?.Invoke();
        }

        [ContextMenu("Invoke Dead Event")]
        private void InvokeDeadEvent()
        {
            _owner.OnDeadEvent?.Invoke();
        }

        private Entity _owner;

        public void Initialize(Entity owner)
        {
            _owner = owner;
        }

#if UNITY_EDITOR
        private void Awake()
        {
            _start = true;
        }

        private void OnValidate()
        {
            if (_start)
            {
                ToolTypeChangeEventInvoke();
            }
        }
#endif
    }
}
