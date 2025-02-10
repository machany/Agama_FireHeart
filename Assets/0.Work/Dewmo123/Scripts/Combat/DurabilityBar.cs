using Agama.Scripts.Entities;
using DG.Tweening;
using Scripts.Structures;
using System;
using UnityEngine;

namespace Scripts.Combat
{
    public class DurabilityBar : MonoBehaviour, IEntityComponent,IAfterInitialize
    {
        private Entity _entity;
        private StructureDurability _durabilityCompo;
        [SerializeField] private Transform _pivotTrm;
        [SerializeField] private float _duration;
        public void Initialize(Entity owner)
        {
            _entity = owner;
        }
        public void AfterInitialize()
        {
            _durabilityCompo = _entity.GetComp<StructureDurability>();
            _durabilityCompo.currentDurability.OnValueChanged += HandleDurabilityChanged;
        }

        private void HandleDurabilityChanged(float prev, float next)
        {
            DOTween.KillAll();
            _pivotTrm.DOScaleX(_durabilityCompo.HealthPercent, _duration);
        }
    }
}
