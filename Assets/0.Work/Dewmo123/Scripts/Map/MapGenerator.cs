using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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
        private void Awake()
        {
            if (_instance == null) _instance = this;
            else Debug.LogWarning("Instance is already existing");

            SetMap();
        }

        private void SetMap()
        {
            for (int i = 0; i < _infos.Count; i++)
            {
                for (int j = 0; j < _infos[i].count; j++)
                {
                    int x = UnityEngine.Random.Range(-_mapSizeX, _mapSizeX);
                    int y = UnityEngine.Random.Range(-_mapSizeY, _mapSizeY);
                    List<Vector3Int> tiles = GetObjectSize(_infos[i].size, x, y);
                    while (!SearchDuplicateTile(tiles))
                    {
                        tiles = GetObjectSize(_infos[i].size, x, y);
                        x = UnityEngine.Random.Range(-_mapSizeX + 1, _mapSizeX);
                        y = UnityEngine.Random.Range(-_mapSizeY + 1, _mapSizeY);
                    }
                    Vector2 pos = _map.CellToWorld(tiles[(int)Mathf.Ceil(tiles.Count / 2)]) + Vector3.one / 2;

                    Instantiate(_infos[i].prefab, pos, Quaternion.identity);

                    foreach (var item in tiles)
                        _map.SetTile(item, _dummy);
                }
            }
        }

        private bool SearchDuplicateTile(List<Vector3Int> tiles)
        {
            foreach (var tile in tiles)
            {
                if (_map.GetTile(tile))
                    return false;
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
                _map.SetTile(tile, _dummy);
                var go = Instantiate(structure, _map.CellToWorld(tile)+Vector3.one/2, Quaternion.identity,transform);
                return true;
            }
            return false;
        }
#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(Vector3.zero, new Vector3(_mapSizeX*2, _mapSizeY*2));
        }
#endif
    }
}
