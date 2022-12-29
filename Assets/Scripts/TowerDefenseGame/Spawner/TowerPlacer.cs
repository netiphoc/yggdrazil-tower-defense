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

        public AbstractTower SelectTower { get; set; }

        private void Awake()
        {
            gameManager.DebugAssert();
            cam.DebugAssert();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var block = GetMouseHoverBlock();
                if (block)
                {
                    TryRemovePlacedObject(block);
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
            var tower = gameManager.TowerSpawner.SpawnEntityType(SelectTower.GetEntityType(), block);
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