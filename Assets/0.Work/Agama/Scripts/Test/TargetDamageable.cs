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
        [field: SerializeField] public IDamageable.DamageMethodType DamageableType { get; private set; } = IDamageable.DamageMethodType.Entity;

        public void ApplyDamage(IDamageable.DamageMethodType damageType, float damage, bool isPowerAttack)
        {
            if (DamageableType == damageType)
            {
                string a = damage > 10 ? "으악!" : "시발!";
                string n = !isPowerAttack && damage < 10 ? "안" : !isPowerAttack ? "꽤" : "ㅈㄴ";
                Debug.Log($"{gameObject.name} : {a} {n} 아파");
            }
            else
                Debug.Log("시원하노");
        }
    }
}
