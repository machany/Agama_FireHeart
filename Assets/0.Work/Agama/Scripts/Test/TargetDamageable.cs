using Agama.Scripts.Combats;
using Agama.Scripts.Entities;
using Scripts.Structures;
using System.Collections.Generic;
using UnityEngine;

namespace Agama.Scripts.Test
{
    public class TargetDamageable : MonoBehaviour, IDamageable, IInteractable
    {
        [field: SerializeField] public DamageMethodType DamageableType { get; private set; } = DamageMethodType.Entity;
        [SerializeField] private List<string> dialogues;
        private int _dialoguesIndex = 0;

        public void ApplyDamage(DamageMethodType damageType, float damage, Entity dealer)
        {
            if (DamageableType == damageType)
                Debug.Log($"[System] : {gameObject.name} damaged {damage}.");
            else
                Debug.Log($"[System] : {gameObject.name}은 꿈쩍도 안한다.");
        }

        public void Interact()
        {
            Debug.Log(dialogues[_dialoguesIndex = Mathf.Min(++_dialoguesIndex, dialogues.Count - 1)]);
        }
    }
}
