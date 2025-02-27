using Baracuda.Threading;
using GGMPool;
using NUnit.Framework;
using NUnit.Framework.Internal.Execution;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;
using Debug = UnityEngine.Debug;

namespace Scripts.Map
{
    [Serializable]
    struct StructureInfo
    {
        public GameObject prefab;
        public int count;
        public Vector2 size;
    }
    public class MapGenerator : MonoBehaviour
    {
        private static MapGenerator _instance;
        public static MapGenerator Instance => _instance;

        [SerializeField] private List<StructureInfo> _infos;
        [SerializeField] private Tilemap _map;
        [SerializeField] private TileBase _dummy;
        [SerializeField] private int _mapSizeX, _mapSizeY;
        private bool[,] _mapArr;
        private System.Random _rand;
        private async void Awake()
        {
            if (_instance == null) _instance = this;
            else Debug.LogWarning("Instance is already existing");
            _rand = new System.Random();
            _mapArr = new bool[_mapSizeX * 2 + 2, _mapSizeY * 2 + 2];
            await Task.Run(SetMap);
        }
        private void SetMap()
        {
            for (int i = 0; i < _infos.Count; i++)
            {
                for (int j = 0; j < _infos[i].count; j++)
                {
                    StructureInfo info = _infos[i];

                    int x = _rand.Next(-_mapSizeX, _mapSizeX);
                    int y = _rand.Next(-_mapSizeY, _mapSizeY);

                    List<Vector3Int> tiles = GetObjectSize(_infos[i].size, x, y);

                    while (!SearchDuplicateTile(tiles))
                    {
                        tiles = GetObjectSize(_infos[i].size, x, y);
                        x = _rand.Next(-_mapSizeX, _mapSizeX);
                        y = _rand.Next(-_mapSizeY, _mapSizeY);
                    }
                    Dispatcher.Invoke(() =>
                    {
                        Vector2 pos = _map.CellToWorld(tiles[(int)Mathf.Ceil(tiles.Count / 2)]) + Vector3.one / 2;
                        Instantiate(info.prefab, pos, Quaternion.identity);
                    });

                    foreach (var item in tiles)
                        _mapArr[item.x + _mapSizeX, item.y + _mapSizeY] = true;
                }
            }
        }
        private bool SearchDuplicateTile(List<Vector3Int> tiles)
        {
            foreach (var tile in tiles)
            {
                try
                {
                    if (_mapArr[tile.x + _mapSizeX, tile.y + _mapSizeY])
                        return false;
                }
                catch (IndexOutOfRangeException ex)
                {
                    Debug.Log(ex.ToString());
                    return false;
                }
            }
            return true;
        }

        private List<Vector3Int> GetObjectSize(Vector2 size, int x, int y)
        {
            List<Vector3Int> sizes = new List<Vector3Int>();
            for (int i = 0; i < size.x; i++)
                for (int j = 0; j < size.y; j++)
                    sizes.Add(new Vector3Int(i + x, j + y));
            return sizes;
        }
        public bool BuildStructure(Vector2 pos, GameObject structure)
        {
            var tile = _map.WorldToCell(pos);
            var list = new List<Vector3Int>();
            list.Add(tile);
            if (SearchDuplicateTile(list))
            {
                _mapArr[tile.x + _mapSizeX, tile.y + _mapSizeY] = true;
                var go = Instantiate(structure, _map.CellToWorld(tile) + Vector3.one / 2, Quaternion.identity, transform);
                return true;
            }
            return false;
        }
        public void DestoryStructure(Vector2 pos)
        {
            var tile = _map.WorldToCell(pos);
            _mapArr[tile.x + _mapSizeX, tile.y + _mapSizeY] = false;
        }
#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(Vector3.zero, new Vector3(_mapSizeX * 2, _mapSizeY * 2));
        }
#endif
    }
}
