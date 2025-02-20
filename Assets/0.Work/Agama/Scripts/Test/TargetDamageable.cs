using Agama.Scripts.Combats;
using Agama.Scripts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Agama.Scripts.Test
{
    public class TargetDamageable : MonoBehaviour, IDamageable
    {
        [field: SerializeField] public DamageMethodType DamageableType { get; private set; } = DamageMethodType.Entity;

        public void ApplyDamage(DamageMethodType damageType, float damage, Entity dealer)
        {
            if (DamageableType == damageType)
            {
                string a = damage > 25 ? "으악! 씨1발2련3이?" : "시발 개1새2끼가?!";
                Debug.Log($"{gameObject.name} : {a}\n[System] : {gameObject.name} damaged {damage}.");
            }
            else
                Debug.Log("그걸로 때려서 간에 기별이나 나겠노?");
        }
    }
}
