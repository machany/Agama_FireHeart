using Agama.Scripts.Entities;
using GGMPool;
using Scripts.Core.Sound;
using System;
using UnityEngine;

namespace Scripts.Players
{
    public class EntitySound : MonoBehaviour, IEntityComponent
    {
        protected EntityAnimatorTrigger _anim;

        [SerializeField] private PoolManagerSO _poolManager;
        [SerializeField] private PoolTypeSO _soundPlayer;
        protected Entity _entity;
        public virtual void Initialize(Entity owner)
        {
            _anim = owner.GetComp<EntityAnimatorTrigger>();
            _entity = owner;
        }


        protected virtual void PlaySound(SoundSO so)
        {
            var sp = _poolManager.Pop(_soundPlayer) as SoundPlayer;
            sp.PlaySound(so,transform.position);
        }
    }
}
