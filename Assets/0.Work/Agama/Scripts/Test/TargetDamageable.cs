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
        public void ApplyDamage(int damage, bool isPowerAttack, Entity dealer)
        {
            string a = damage > 10 ? "으악!" : "시발!";
            string n = !isPowerAttack && damage < 10 ? "안" : !isPowerAttack ? "꽤" : "ㅈㄴ";
            Debug.Log($"{gameObject.name} : {a} {n} 아파");
        }
    }
}
