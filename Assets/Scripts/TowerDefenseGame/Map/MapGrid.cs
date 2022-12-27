using System;
using UnityEngine;

namespace TowerDefenseGame.Map
{
    public class MapManager : MonoBehaviour
    {
        private const float GridCellSize = 1f;

        [Header("Map")] [SerializeField] private MapSo mapSo;
        [SerializeField] private BlockPrefabSo blockPrefabSo;
        public Grid<Block> Grid { get; private set; }

        private void Awake()
        {
            Grid = MapGeneratorUtility.GenerateMap(mapSo, blockPrefabSo, GridCellSize);
        }
    }
}