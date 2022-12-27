using UnityEngine;

namespace TowerDefenseGame.Map
{
    [CreateAssetMenu(fileName = "New " + nameof(BlockPrefabSo), menuName = "Tower Defense/Map/" + nameof(BlockPrefabSo),
        order = 0)]
    public class BlockPrefabSo : ScriptableObject
    {
        [SerializeField] private Block defaultBlock;

        [SerializeField] private BlockPrefabData[] blockPrefabs;

        public Block GetBlockPrefab(Color blockColor)
        {
            var hexColor = $"#{ColorUtility.ToHtmlStringRGB(blockColor)}";
            foreach (var blockPrefab in blockPrefabs)
            {
                if (!blockPrefab.hexColorCode.Equals(hexColor)) continue;
                return blockPrefab.blockPrefab;
            }

            return defaultBlock;
        }
    }
}