using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Agama.Scripts.Entities.FSM
{
    [CreateAssetMenu(fileName = "StateList", menuName = "SO/FSM/StateList", order = 1)]
    public class EntityStateSOList : ScriptableObject
    {
        public EntityStateSO defaultState;
        public List<EntityStateSO> entityStates;

        private void OnValidate()
        {
            if (defaultState == null)
            {
                Debug.LogError($"{this.name} : default state is null!");
                return;
            }

            if (!entityStates.Contains(defaultState))
            {
                Debug.Log($"{this.name} : state list include not default state. so state list add default state");
                entityStates.Add(defaultState);
            }
        }
    }
}
