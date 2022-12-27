using UnityEngine;

namespace TowerDefenseGame.Map.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New " + nameof(BlockTypeSo), menuName = "Tower Defense/" + nameof(BlockTypeSo),
        order = 0)]
    public class BlockTypeSo : ScriptableObject
    {
        [SerializeField] private string hexColorCode;
        public string HexColorCode => hexColorCode;
    }
}