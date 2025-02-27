using Agama.Scripts.Enemies;
using Agama.Scripts.Entities;
using System;
using System.Collections.Generic;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace Agama.Scripts.Behavior.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "Move to StartPosition", story: "[Mover] move to startPosition of [BehaviorEnemy] and flip [Renderer]", category: "Action", id: "d49b2b1c0ca0b9e35aafdb8843211cf4")]
    public partial class MoveToStartPositionAction : Action
    {
        [SerializeReference] public BlackboardVariable<EntityMover> Mover;
        [SerializeReference] public BlackboardVariable<BehaviorEnemy> BehaviorEnemy;
        [SerializeReference] public BlackboardVariable<EntityRenderer> Renderer;

        private Stack<Core.AStar.Node> _road;
        private Agama.Scripts.Core.AStar.Node _currentNode;
        private bool _arriveFrag;

        protected override Status OnStart()
        {
            _road = BehaviorEnemy.Value.roadFinder[BehaviorEnemy.Value.transform];

            _currentNode = _road.Pop();

            Mover.Value.SetMoveFor(_currentNode.worldPosition, () => _arriveFrag = true);

            Vector2 direction = _currentNode.worldPosition - (Vector2)BehaviorEnemy.Value.transform.position;
            Renderer.Value.Flip(direction.x);
            _arriveFrag = false;

            return Status.Running;
        }

        protected override Status OnUpdate()
        {
            if (_arriveFrag)
            {
                _arriveFrag = false;
                try
                {
                    _currentNode = _road.Pop(); // 만약 정보를 빼내는 실패함
                }
                catch // 다른 변수로 오류가 생겼을 수 있으니, 대비함.
                {
                }

                Mover.Value.SetMoveFor(_currentNode.worldPosition, () => _arriveFrag = true);

                Vector2 direction = _currentNode.worldPosition - (Vector2)BehaviorEnemy.Value.transform.position;
                Renderer.Value.Flip(direction.x);
            }

            return Status.Running;
        }
    }
}
