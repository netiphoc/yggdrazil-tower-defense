using TMPro;
using TowerDefenseGame.Map;
using UnityEngine;

namespace TowerDefenseGame.UI
{
    public class UIBlockID : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI blockIdText;

        public void SetBlockId(Block block)
        {
            blockIdText.SetText($"x:{block.X} y:{block.Y}");
        }
    }
}