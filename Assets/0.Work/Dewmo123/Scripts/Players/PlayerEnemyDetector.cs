using Agama.Scripts.Core;
using Agama.Scripts.Entities;
using Agama.Scripts.Players;
using System;
using UnityEngine;

namespace Scripts.Players
{
    public class PlayerEnemyDetector : MonoBehaviour, IEntityComponent
    {
        private PlayerInputSO _input;
        private Player _player; 
        public void Initialize(Entity owner)
        {
            _player = owner as Player;
            _input = _player.InputSO;
        }

    }
}
