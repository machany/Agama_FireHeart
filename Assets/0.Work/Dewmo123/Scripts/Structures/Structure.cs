using Agama.Scripts.Entities;
using Scripts.EventChannel;
using UnityEngine;

namespace Scripts.Structures
{
    public abstract class Structure : Entity
    {
        //HP 갖고있음
        public abstract void Activate();
    }
}
