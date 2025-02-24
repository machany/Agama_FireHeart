using System.Collections.Generic;
using UnityEngine;

namespace Agama.Scripts.Core.AStar
{
    public class RoadFinder : MonoBehaviour
    {
        [SerializeField] private Grid grid;

        private Dictionary<Transform, Stack<Node>> _pathOfRequesterDictionary;

        public Stack<Node> this[Transform requestor]
            => _pathOfRequesterDictionary[requestor];

        private void Awake()
        {
            grid = transform.GetComponent<Grid>();
        }

        public void FindPath(Transform requestor, Vector3 targetPos)
        {
            Vector3 startPos = requestor.position;

            Node startNode = grid.GetNodeFromPosition(startPos);
            Node targetNode = grid.GetNodeFromPosition(targetPos);

            Debug.Assert(startNode != null && targetNode != null, "StartNode or TargetNode is null");

            List<Node> openSet = new List<Node>(); // 탐색할 노드 목록  
            HashSet<Node> closedSet = new HashSet<Node>(); // 이미 탐색된 노드 목록

            openSet.Add(startNode); // 시작 노드 추가

            while (openSet.Count > 0) // 노드가 남아 있으면 반복
            {
                Node currentNode = openSet[0];

                for (int i = 1; i < openSet.Count; i++)
                {
                    if (openSet[i].TotalCost < currentNode.TotalCost || openSet[i].TotalCost == currentNode.TotalCost &&
                        openSet[i].euclidStreetCost < currentNode.euclidStreetCost)
                    {
                        currentNode = openSet[i];
                    }
                }

                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                if (currentNode == targetNode)
                {
                    _pathOfRequesterDictionary[requestor] = RetracePath(startNode, targetNode, _pathOfRequesterDictionary[requestor]); // 경로 추적
                    return;
                }

                SeaechAdjacentNodes(currentNode, targetNode, openSet, closedSet);
            }
        }

        // 인접 노드 탐색
        private void SeaechAdjacentNodes(Node currentNode, Node targetNode, List<Node> openSet, HashSet<Node> closedSet)
        {
            foreach (Node neighbour in grid.GetAdjacentNodes(currentNode))
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

        private Stack<Node> RetracePath(Node startNode, Node endNode, Stack<Node> path = null)
        {
            path ??= new Stack<Node>();
            path.Clear();
            Node currentNode = endNode;

            while (currentNode != startNode)
            {
                path.Push(currentNode);
                currentNode = currentNode.parent;

                Debug.Assert(currentNode != null, "Parent Node is null while Retracing path");
            }

            // 역순으로 추가해서 스택사용
            path.Push(currentNode);

            if (path.Count == 0)
                return null;

            return path;
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