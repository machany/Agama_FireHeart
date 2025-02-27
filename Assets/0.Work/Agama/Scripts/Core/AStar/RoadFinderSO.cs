using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Agama.Scripts.Core.AStar
{
    [CreateAssetMenu(fileName = "RoadFinderSO", menuName = "SO/RoadFinder", order = 0)]
    public class RoadFinderSO : ScriptableObject
    {
        [SerializeField] private LayerMask unPassLayer;
        [SerializeField] private Vector2 gridSize;
        [SerializeField] private float nodeRadius;

        private Grid _grid;
        private Dictionary<Transform, Stack<Node>> _pathOfRequesterDictionary;
        private  List<Node> _openSet;
        private HashSet<Node> _closedSet;

        public Stack<Node> this[Transform requestor]
            => _pathOfRequesterDictionary[requestor];

        public void Initialize()
        {
            _pathOfRequesterDictionary = new Dictionary<Transform, Stack<Node>>();

            GameObject gameObject = new GameObject("Road Finder");
            _grid = gameObject.transform.AddComponent<Grid>();
            _grid.Initialize(unPassLayer, gridSize, nodeRadius);

            _openSet = new List<Node>();
            _closedSet = new HashSet<Node>();
        }

        public void ChangeGrid()
        {
            _grid.ChangeGrid();
        }

        public bool FindPath(Transform requestor, Vector3 targetPos)
        {
            if (!_pathOfRequesterDictionary.ContainsKey(requestor))
                _pathOfRequesterDictionary.Add(requestor, new Stack<Node>());

            Vector3 startPos = requestor.position;

            Node startNode = _grid.GetNodeFromPosition(startPos);
            Node targetNode = _grid.GetNodeFromPosition(targetPos);

            Debug.Assert(startNode != null && targetNode != null, "StartNode or TargetNode is null");

            _openSet.Clear(); // 탐색할 노드 목록  
            _closedSet.Clear(); // 이미 탐색된 노드 목록

            _openSet.Add(startNode); // 시작 노드 추가

            while (_openSet.Count > 0) // 노드가 남아 있으면 반복
            {
                Node currentNode = _openSet[0];

                for (int i = 1; i < _openSet.Count; i++)
                {
                    if (_openSet[i].TotalCost < currentNode.TotalCost || _openSet[i].TotalCost == currentNode.TotalCost &&
                        _openSet[i].euclidStreetCost < currentNode.euclidStreetCost)
                    {
                        currentNode = _openSet[i];
                    }
                }

                _openSet.Remove(currentNode);
                _closedSet.Add(currentNode);

                if (currentNode == targetNode)
                    return RetracePath(startNode, targetNode, requestor); // 경로 추적

                SeaechAdjacentNodes(currentNode, targetNode, _openSet, _closedSet);
            }

            return true;
        }

        // 인접 노드 탐색
        private void SeaechAdjacentNodes(Node currentNode, Node targetNode, List<Node> openSet, HashSet<Node> closedSet)
        {
            foreach (Node neighbour in _grid.GetAdjacentNodes(currentNode))
            {
                if (!neighbour.canPass || closedSet.Contains(neighbour))
                    continue;

                int newCostToNeighbour = currentNode.forendDistanceCost + GetDistance(currentNode, neighbour);

                if (newCostToNeighbour < neighbour.forendDistanceCost || !openSet.Contains(neighbour))
                {
                    neighbour.forendDistanceCost = newCostToNeighbour;
                    neighbour.euclidStreetCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }

        private bool RetracePath(Node startNode, Node endNode, Transform key)
        {
            Stack<Node> path = _pathOfRequesterDictionary[key];
            path.Clear();
            Node currentNode = endNode;

            while (currentNode != startNode)
            {
                path.Push(currentNode);
                currentNode = currentNode.parent;

                Debug.Assert(currentNode != null, "Parent Node is null while Retracing path");
            }

            if (path.Count == 0)
                return false;

            return true;
        }

        private int GetDistance(Node nodeA, Node nodeB)
        {
            // 두 노드 간의 거리 계산
            int dstX = Mathf.Abs(nodeA.xIndex - nodeB.xIndex);
            int dstY = Mathf.Abs(nodeA.yIndex - nodeB.yIndex);

            // 대각선 이동이 더 비용이 크므로 우선적으로 계산
            if (dstX > dstY)
                return 14 * dstY + 10 * (dstX - dstY);
            return 14 * dstX + 10 * (dstY - dstX);
        }
    }
}