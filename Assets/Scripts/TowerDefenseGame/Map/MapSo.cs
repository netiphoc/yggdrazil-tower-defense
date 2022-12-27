using System;
using UnityEngine;

namespace TowerDefenseGame.Map
{
    [CreateAssetMenu(fileName = "New " + nameof(MapSo), menuName = "Tower Defense/" + nameof(MapSo), order = 0)]
    public class MapSo : ScriptableObject
    {
        [Serializable]
        public struct BlockPrefabData
        {
            public string hexColorCode;
            public Block blockPrefab;
        }

        [Tooltip(
            "[CONFIG]\nFile Format: PSD,PNG,JPG\nNon-Power of 2: None\nRead/Write: Checked\nWrap Mode: Clamp\nFilter Mode: Point (no filter)\nResize Algorithm: Bilinear")]
        [SerializeField]
        private Texture2D mapTexture2D;

        [Header("Block Prefabs")] [SerializeField]
        private Block defaultBlock;

        [SerializeField] private BlockPrefabData[] blockPrefabs;
        public Texture2D MapTex => mapTexture2D;

        public Block GetBlockPrefab(Color blockColor)
        {
            var hexColor = $"#{ColorUtility.ToHtmlStringRGB(blockColor).ToLower()}";
            foreach (var blockPrefab in blockPrefabs)
            {
                if (!blockPrefab.hexColorCode.ToLower().Equals(hexColor)) continue;
                return blockPrefab.blockPrefab;
            }

            return defaultBlock;
        }
    }
}