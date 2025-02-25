using System;
using System.Collections.Generic;
using UnityEngine;

namespace Agama.Scripts.Core.AStar
{
    public class Grid : MonoBehaviour
    {
        private Node[,] _grid;

        private LayerMask _unPassLayer;
        private Vector2 _gridSize;
        private float _nodeRadius;

        private float _nodeSize;
        private int _gridSizeX, _gridSizeY;

        public void Initialize(LayerMask unPassLayer, Vector2 gridSize, float nodeRadius)
        {
            _unPassLayer = unPassLayer;
            _gridSize = gridSize;
            _nodeRadius = nodeRadius;

            SetNodeAndGrid();
            InitializeGrid();
        }

        private void SetNodeAndGrid()
        {
            if (_nodeRadius <= 0)
                throw new ArgumentOutOfRangeException();

            _nodeSize = _nodeRadius * 2;
            _gridSizeX = Mathf.RoundToInt(_gridSize.x / _nodeSize);
            _gridSizeY = Mathf.RoundToInt(_gridSize.y / _nodeSize);
            Debug.Log(_nodeSize + " // " + _gridSize + " = " + _gridSizeX + " , " + _gridSizeY);

            if (_gridSizeX <= 0 || _gridSizeY <= 0)
                throw new ArgumentOutOfRangeException();
        }

        private void InitializeGrid()
        {
            _grid = new Node[_gridSizeX, _gridSizeY];
            for (int x = 0; x < _gridSizeX; x++)
            {
                for (int y = 0; y < _gridSizeY; y++)
                {
                    _grid[x, y] = new Node(true, Vector2.zero, x, y);
                }
            }

            ChangeGrid();
        }

        [ContextMenu("ChangeGrid")]
        public void ChangeGrid()
        {
            Vector2 startPosition = (Vector2)transform.position - Vector2.right * _gridSize.x / 2 - Vector2.up * _gridSize.y / 2;

            for (int x = 0; x < _gridSizeX; x++)
            {
                for (int y = 0; y < _gridSizeY; y++)
                {
                    Vector2 nodePosition = startPosition + Vector2.right * (x * _nodeSize + _nodeRadius) + Vector2.up * (y * _nodeSize + _nodeRadius);
                    bool canPass = !Physics2D.OverlapCircle(nodePosition, _nodeRadius, _unPassLayer);

                    Node currentNode = _grid[x, y];

                    currentNode.canPass = canPass;
                    currentNode.worldPosition = nodePosition;
                }
            }
        }

        public Node GetNodeFromPosition(Vector3 position)
        {
            if (_grid == null)
            {
                return null;
            }

            // 비율 계산
            float percentX = (position.x + _gridSize.x / 2) / _gridSize.x;
            float percentY = (position.y + _gridSize.y / 2) / _gridSize.y;

            percentX = Mathf.Clamp01(percentX);
            percentY = Mathf.Clamp01(percentY);

            int x = Mathf.RoundToInt((_gridSizeX - 1) * percentX);
            int y = Mathf.RoundToInt((_gridSizeY - 1) * percentY);

            x = Mathf.Clamp(x, 0, _gridSizeX - 1);
            y = Mathf.Clamp(y, 0, _gridSizeY - 1);

            return _grid[x, y];
        }

        public List<Node> GetAdjacentNodes(Node node)
        {
            List<Node> adjacentNodes = new List<Node>();

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    // 자기 자신은 제외
                    if (x == 0 && y == 0)
                        continue;

                    int checkX = node.xIndex + x;
                    int checkY = node.yIndex + y;

                    // 그리드 범위 내인지 확인
                    if (checkX >= 0 && checkX < _gridSizeX &&
                        checkY >= 0 && checkY < _gridSizeY)
                    {
                        adjacentNodes.Add(_grid[checkX, checkY]);
                    }
                }
            }

            return adjacentNodes;
        }

#if UNITY_EDITOR

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan; // 전체 그리드
            Gizmos.DrawWireCube(transform.position, new Vector2(_gridSize.x, _gridSize.y));

            if (_grid != null)
            {
                foreach (Node node in _grid)
                {
                    Gizmos.color = node.canPass ? Color.white : Color.red;  // 못 가면 빨강 갈 수 있으면 하양
                    Gizmos.DrawWireCube(node.worldPosition, new Vector2(_nodeSize, _nodeSize));
                }
            }
        }

#endif
    }
}
