using System;
using UnityEngine;

namespace TowerDefenseGame.Map
{
    public class MapManager : MonoBehaviour
    {
        [Header("Map")] [SerializeField] private MapSo mapSo;
        public Grid<Block> Grid { get; private set; }

        private void Awake()
        {
            Grid = MapGeneratorUtility.GenerateMap(mapSo);
        }
    }
}