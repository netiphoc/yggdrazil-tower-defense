using TowerDefenseGame.GameEntity;
using TowerDefenseGame.Map;
using UnityEngine;
using Utilities;

namespace TowerDefenseGame.Spawner
{
    public class TowerPlacer : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private Camera cam;

        private int _currentTowerTypeIndex;

        private void Awake()
        {
            gameManager.DebugAssert();
            cam.DebugAssert();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _currentTowerTypeIndex = 0;
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                _currentTowerTypeIndex = 1;
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                _currentTowerTypeIndex = 2;
            }

            if (Input.GetMouseButtonDown(0))
            {
                var block = GetMouseHoverBlock();
                if (block)
                {
                    TryPlaceTower(block);
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                var block = GetMouseHoverBlock();
                if (block)
                {
                    TryRemovePlacedObject(block);
                }
            }
        }

        private void TryPlaceTower(Block block)
        {
            if (block is not TowerBlock) return;
            if (block.PlacedObject) return;
            var towerType = gameManager.TowerSpawner.GetTowers()[_currentTowerTypeIndex];
            var tower = gameManager.TowerSpawner.SpawnEntityType(towerType, block);
            block.PlacedObject = tower.gameObject;
            this.Log($"Tower placed: x:{block.X} y:{block.Y}");
        }

        private void TryRemovePlacedObject(Block block)
        {
            if (block is not TowerBlock) return;
            if (!block.PlacedObject) return;
            gameManager.TowerSpawner.DeSpawn(block.PlacedObject.GetComponent<AbstractTower>());
            block.PlacedObject = null;
            this.Log($"Tower removed: x:{block.X} y:{block.Y}");
        }

        private Block GetMouseHoverBlock()
        {
            var ray = cam.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out var hit, 100f)) return null;
            return hit.transform.TryGetComponent(out Block block) ? block : null;
        }
    }
}