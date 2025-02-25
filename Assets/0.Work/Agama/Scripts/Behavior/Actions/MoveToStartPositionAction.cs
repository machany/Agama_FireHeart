using Agama.Scripts.Enemies;
using Agama.Scripts.Entities;
using System;
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

        private Agama.Scripts.Core.AStar.Node _currentNode;
        private bool _arriveFrag;

        protected override Status OnStart()
        {
            BehaviorEnemy.Value.roadFinder.FindPath(BehaviorEnemy.Value.transform, BehaviorEnemy.Value.StartPosition);
            _currentNode = BehaviorEnemy.Value.roadFinder[BehaviorEnemy.Value.transform].Pop();

            Mover.Value.SetMoveFor(_currentNode.worldPosition, () => _arriveFrag = true);

            Vector2 direction = _currentNode.worldPosition - (Vector2)BehaviorEnemy.Value.transform.position;
            Renderer.Value.Flip(direction.x);
            _arriveFrag = false;

            return Status.Running;
        }

        protected override Status OnUpdate()
        {
            Debug.Log(Vector2.Distance(_currentNode.worldPosition, BehaviorEnemy.Value.transform.position));
            float distance = Vector2.Distance(_currentNode.worldPosition, BehaviorEnemy.Value.transform.position);

            if (Vector2.Distance(BehaviorEnemy.Value.StartPosition, BehaviorEnemy.Value.transform.position) <= .1f)
            {
                return Status.Success;
            }
            else if (_arriveFrag)
            {
                _arriveFrag = false;

                if (!BehaviorEnemy.Value.roadFinder.FindPath(BehaviorEnemy.Value.transform, BehaviorEnemy.Value.StartPosition)) // 길을 못 찾으면
                {
                    return Status.Failure;
                }
                _currentNode = BehaviorEnemy.Value.roadFinder[BehaviorEnemy.Value.transform].Pop();

                Mover.Value.SetMoveFor(_currentNode.worldPosition, () => _arriveFrag = true);

                Vector2 direction = _currentNode.worldPosition - (Vector2)BehaviorEnemy.Value.transform.position;
                Renderer.Value.Flip(direction.x);
            }

            return Status.Running;
        }
    }
}
