using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TowerDefenseGame.Map
{
    public static class MapGeneratorUtility
    {
        private const float GridCellSize = 1f;

        public static Grid<Block> GenerateMap(MapSo map)
        {
            var mapWidth = map.MapTex.width;
            var mapHeight = map.MapTex.height;

            var grid = new Grid<Block>(mapWidth, mapHeight, GridCellSize);

            for (var x = 0; x < mapWidth; x++)
            {
                for (var y = 0; y < mapHeight; y++)
                {
                    var color = map.MapTex.GetPixel(x, y);
                    var block = Object.Instantiate(map.GetBlockPrefab(color));
                    var posX = x * GridCellSize;
                    var posZ = y * GridCellSize;
                    block.transform.position = new Vector3(posX, 0, posZ);
                    grid.SetElementAt(x, y, block);
                }
            }

            return grid;
        }
    }
}