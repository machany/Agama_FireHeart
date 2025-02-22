using Agama.Scripts.Animators;
using Agama.Scripts.Core;
using Agama.Scripts.Entities;
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
                if (swordCombo > swordMaxCombo)
                    swordCombo = swordMinCombo;
                else if (swordCombo < swordMinCombo)
                    swordCombo = swordMaxCombo;
            }
        }


        private Player _player;
        private EntityMover _mover;
        private EntityRenderer _renderer;
        private EntityStat _statComp;

        public override void Initialize(Entity owner)
        {
            base.Initialize(owner);

            damagecaster.InitCaster(owner);
            _player = owner as Player;

            _renderer = _player.GetComp<EntityRenderer>();
            _mover = _player.GetComp<EntityMover>();
            _statComp = _player.GetComp<EntityStat>();

            _player.OnQuickSloatItemChange += HandleToolTypeChanged;
        }

        private void OnDestroy()
        {
            _player.OnQuickSloatItemChange -= HandleToolTypeChanged;
        }

        private void HandleToolTypeChanged(sbyte toolType, float attackPower)
        {
            damagecaster.ChangeDamageType(toolType);
            _statComp.SetBaseValue(attackPowerStat, attackPower);
        }

        public void UseToolComboChanged()
        {
            if (_player.ToolType == swordToolType)
                _renderer.SetParamiter(swordComboParam, SwordCombo++);
        }

        public override void Attack()
        {
            base.Attack();

            damagecaster.CastDamage(_statComp.GetStat(attackPowerStat).BaseValue);
        }

        private void Update()
        {
            damagecaster.UpdateCaster();
            if (_mover.CanMove)
                    transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, Mathf.Atan2(-_player.InputSO.PreviousInputVector.x, _player.InputSO.PreviousInputVector.y) * (180 / Mathf.PI));
        }
    }
}
