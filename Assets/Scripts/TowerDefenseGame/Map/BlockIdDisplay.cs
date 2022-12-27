using System;
using TowerDefenseGame.UI;
using UnityEngine;
using Utilities;

namespace TowerDefenseGame.Map
{
    public class BlockIdDisplay : MonoBehaviour
    {
        [SerializeField] private MapManager mapManager;
        [SerializeField] private UIBlockID uiBlockIDPrefab;

        private void Awake()
        {
            mapManager.DebugAssert();
        }

        private void OnEnable()
        {
            mapManager.onGridCreated.AddListener(OnGridCreated);
        }

        private void OnDisable()
        {
            mapManager.onGridCreated.RemoveListener(OnGridCreated);
        }

        private void OnGridCreated(Grid<Block> grid)
        {
            InitializeGridDisplay(grid);
        }

        private void InitializeGridDisplay(Grid<Block> grid)
        {
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