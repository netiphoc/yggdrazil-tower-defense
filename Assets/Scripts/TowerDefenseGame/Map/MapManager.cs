using System;
using TowerDefenseGame.Map.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace TowerDefenseGame.Map
{
    public class MapManager : MonoBehaviour
    {
        public UnityEvent<Grid<Block>> onGridCreated;

        [Header("Map")] [SerializeField] private MapSo mapSo;
        public Grid<Block> Grid { get; private set; }

        private void Awake()
        {
            InitializeGrid();
        }

        private void InitializeGrid()
        {
            Grid = MapGeneratorUtility.GenerateMap(mapSo);
            onGridCreated?.Invoke(Grid);
        }
    }
}