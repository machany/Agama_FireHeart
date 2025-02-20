using Agama.Scripts.Entities;
using DG.Tweening;
using Scripts.Stats;
using Scripts.Structures;
using System;
using UnityEngine;

namespace Scripts.Combat
{
    public class StatBar : MonoBehaviour, IEntityComponent,IAfterInitialize
    {
        private Entity _entity;
        private Stat _statCompo;
        [SerializeField] private Transform _pivotTrm;
        [SerializeField] private float _duration;
        public void Initialize(Entity owner)
        {
            _entity = owner;
        }
        public void AfterInitialize()
        {
            _statCompo = _entity.GetComp<StructureDurability>();
            _statCompo.currentStat.OnValueChanged += HandleStatChange;
            gameObject.SetActive(false);
        }
        private void HandleStatChange(float prev, float next)
        {
            DOTween.Kill(_pivotTrm);
            _pivotTrm.DOScaleX(_statCompo.StatPercent, _duration);
        }
    }
}
