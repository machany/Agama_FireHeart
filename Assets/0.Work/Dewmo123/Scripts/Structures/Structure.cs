using Scripts.EventChannel;
using UnityEngine;

namespace Scripts.Structures
{
    public abstract class Structure : MonoBehaviour
    {
        //HP 갖고있음
        public UIType myType;
        public abstract void Activate();
    }
}
