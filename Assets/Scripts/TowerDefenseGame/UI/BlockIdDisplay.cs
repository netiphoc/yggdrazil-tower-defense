using UnityEngine;
using Utilities;

namespace TowerDefenseGame.UI
{
    public class BlockIdDisplay : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private UIBlockID uiBlockIDPrefab;

        private void Awake()
        {
            gameManager.DebugAssert();
        }

        private void Start()
        {
            InitializeGridDisplay();
        }

        private void InitializeGridDisplay()
        {
            var grid = gameManager.MapManager.Grid;
            for (var x = 0; x < grid.Width; x++)
            {
                for (var y = 0; y < grid.Height; y++)
                {
                    var block = grid.GetElementAt(x, y);
                    var blockPos = block.transform.position;
                    var uiBlockID = Instantiate(uiBlockIDPrefab, transform);
                    uiBlockID.SetBlockId(block);
                    uiBlockID.transform.position = blockPos;
                }
            }
        }
    }
}