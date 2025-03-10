﻿using DG.Tweening;
using Library;
using System;
using UnityEngine;

namespace Agama.Scripts.Entities
{
    public class EntityMover : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private float moveSpeed;

        public bool CanMove { get; set; } = true;

        private float _moveSpeedMultiplier = 1;

        private Vector2 _movementVector;

        private Rigidbody2D _ridComp;
        private Entity _owner;

        public void Initialize(Entity owner)
        {
            _ridComp = transform.parent.GetOrSetComponent<Rigidbody2D>(rid =>
            {
                rid.freezeRotation = true;
                rid.gravityScale = 0;
            });
            CanMove = true;
            _movementVector = Vector2.zero;
            _moveSpeedMultiplier = 1;
            _owner = owner;
        }

        public void SetMoveSpeedMultiplier(float value)
            => _moveSpeedMultiplier = value;

        private void FixedUpdate()
        {
            if (CanMove)
                _ridComp.linearVelocity = _movementVector * moveSpeed * _moveSpeedMultiplier;
        }

        public void SetMovementX(float xMovement)
            => _movementVector.x = Mathf.Abs(xMovement) > 0 ? Mathf.Sign(xMovement) : 0;
        public void SetMovementY(float yMovement)
            => _movementVector.y = Mathf.Abs(yMovement) > 0 ? Mathf.Sign(yMovement) : 0;
        public void SetMovement(Vector2 movement)
        {
            SetMovementX(movement.x);
            SetMovementY(movement.y);
            _movementVector.Normalize();
        }

        public void SetMoveFor(Vector2 goal, Action completeAction = null)
        {
            StopImmediately();
            _owner.transform.DOMove(goal, Vector2.Distance(_owner.transform.position, goal) / (moveSpeed * _moveSpeedMultiplier)).OnComplete(() => completeAction?.Invoke());
        }

        public void StopImmediately()
        {
            _ridComp.linearVelocity = Vector2.zero;
            _movementVector = Vector2.zero;
        }
    }
}
