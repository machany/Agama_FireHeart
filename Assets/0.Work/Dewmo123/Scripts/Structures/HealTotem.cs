using Agama.Scripts.Core;
using Agama.Scripts.Entities;
using UnityEngine;

namespace Scripts.Structures
{
    public class HealTotem : Structure
    {
        [SerializeField] private LayerMask _targetLayer;
        [SerializeField] private float _healRad;
        [SerializeField] private float _healAmount;
        [SerializeField] private float _healDelay;
        private float _curTime;

        private void Heal()
        {
            var targets = Physics2D.OverlapCircleAll(transform.position, _healRad, _targetLayer);
            foreach (var item in targets)
            {
                item.GetComponentInChildren<EntityHealth>().ApplyHeal(_healAmount);
            }
        }
        private void Update()
        {
            _curTime += Time.deltaTime;
            if (_curTime >= _healDelay)
            {
                Heal();
                _curTime = 0;
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _healRad);
        }
#endif
    }
}
