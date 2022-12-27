using System;
using TowerDefenseGame.Map.ScriptableObjects;

namespace TowerDefenseGame.Map
{
    public class MapManager
    {
        public Grid<Block> Grid { get; }

        public MapManager(MapSo mapSo)
        {
            Grid = MapGeneratorUtility.GenerateMap(mapSo);
        }
    }
}