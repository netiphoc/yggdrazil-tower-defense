using System;
using UnityEngine;

namespace TowerDefenseGame.Map.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New " + nameof(MapSo), menuName = "Tower Defense/" + nameof(MapSo), order = 0)]
    public class MapSo : ScriptableObject
    {
        [Serializable]
        public struct BlockPrefabData
        {
            public BlockTypeSo blockTypeSo;
            public Block blockPrefab;
        }

        [SerializeField] private MapConfigSo mapConfigSo;
        [Space] [SerializeField] private Block defaultBlock;
        [SerializeField] private BlockPrefabData[] blockPrefabs;

        public MapConfigSo GetMapConfigSo()
        {
            return mapConfigSo;
        }

        public Block GetBlockPrefab(Color blockColor)
        {
            var hexColor = $"#{ColorUtility.ToHtmlStringRGB(blockColor).ToLower()}";
            foreach (var blockPrefab in blockPrefabs)
            {
                if (!blockPrefab.blockTypeSo.HexColorCode.ToLower().Equals(hexColor)) continue;
                return blockPrefab.blockPrefab;
            }

            return defaultBlock;
        }
    }
}