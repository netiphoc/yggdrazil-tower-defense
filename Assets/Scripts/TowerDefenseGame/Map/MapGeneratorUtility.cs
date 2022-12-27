using System;
using UnityEngine;

namespace TowerDefenseGame.Map
{
    public static class MapGeneratorUtility
    {
        public static Grid<Block> GenerateMap(MapSo map, BlockPrefabSo blockPrefab, float gridCellSize)
        {
            var mapTex = map.MapRawImage.texture as Texture2D;
            if (mapTex == null) throw new Exception("Invalid map image.");
            var mapPixelData = mapTex.GetPixels();
            var colorSize = mapPixelData.Length;

            var grid = new Grid<Block>(map.Width, map.Height, gridCellSize);

            var colorIndex = 0;
            for (var x = 0; x < map.Width; x++)
            {
                for (var y = 0; y < map.Height; y++)
                {
                    if (colorIndex >= colorSize)
                    {
                        throw new Exception("Image width and height doesn't match pixel size.");
                    }

                    var color = mapPixelData[colorIndex++];
                    var block = blockPrefab.GetBlockPrefab(color);
                    grid.SetElementAt(x, y, block);
                }
            }

            return grid;
        }
    }
}