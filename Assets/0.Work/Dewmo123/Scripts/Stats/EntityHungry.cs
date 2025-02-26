using UnityEngine;

namespace Scripts.Stats
{
    public class EntityHungry : Stat
    {
        [SerializeField] private float _decrementPerFrame;

        private void FixedUpdate()
        {
            ApplyDamage(_decrementPerFrame);
        }
    }
}
