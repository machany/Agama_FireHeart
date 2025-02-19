using Agama.Scripts.Animators;
using Agama.Scripts.Combats;
using Agama.Scripts.Core;
using Agama.Scripts.Entities;
using System;
using UnityEngine;

namespace Agama.Scripts.Players
{
    public class PlayerAttackComponent : EntityAttackComponent
    {
        [SerializeField] private AnimationParamiterSO swordComboParam;
        [SerializeField] private StatSO attackPowerStat;
        [SerializeField] private int swordToolType, swordMinCombo, swordMaxCombo;

        private int swordCombo = 1;
        private int SwordCombo
        {
            get => swordCombo;
            set
            {
                swordCombo = value;
                Debug.Log(swordCombo);
                if (swordCombo > swordMaxCombo)
                    swordCombo = swordMinCombo;
                else if (swordCombo < swordMinCombo)
                    swordCombo = swordMaxCombo;
            }
        }

        private Player _player;
        private EntityRenderer _renderer;
        private EntityStat _statComp;

        public override void Initialize(Entity owner)
        {
            base.Initialize(owner);

            damagecaster.InitCaster(owner);
            _player = owner as Player;

            _renderer = _player.GetComp<EntityRenderer>();
            _statComp = _player.GetComp<EntityStat>();

            _player.OnToolTypeChanged += HandleToolTypeChanged;
        }

        private void OnDestroy()
        {
            _player.OnToolTypeChanged -= HandleToolTypeChanged;
        }

        private void HandleToolTypeChanged(DamageMethodType toolType, float attackpower)
        {
            damagecaster.ChangeDamageType(toolType);
            _statComp.SetBaseValue(attackPowerStat, attackpower);
        }

        public void UseToolComboChanged()
        {
            if (_player.ToolType == swordToolType)
                _renderer.SetParamiter(swordComboParam, SwordCombo++);
        }

        public override void Attack()
        {
            base.Attack();

            damagecaster.CastDamage(1, true);
        }

        private void Update()
        {
            damagecaster.UpdateCaster();

            if (_player.InputSO.MoveInputVector.magnitude <= Mathf.Epsilon)
                transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, -90 * _renderer.FacingDirection);
            else
                transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, Mathf.Atan2(-_player.InputSO.MoveInputVector.x, _player.InputSO.MoveInputVector.y) * (180 / Mathf.PI));
        }
    }
}
