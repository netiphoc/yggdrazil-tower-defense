﻿using TowerDefenseGame.GameEntity;
using TowerDefenseGame.Map;
using UnityEngine;
using Utilities;

namespace TowerDefenseGame.Spawner
{
    public class TowerPlacer : MonoBehaviour
    {
        [SerializeField] private GameController gameController;
        [SerializeField] private Camera cam;

        public AbstractTower SelectTower { get; set; }

        public bool CanPlace { get; set; }
        public bool HasTower => SelectTower;

        private void Awake()
        {
            gameController.DebugAssert();
            cam.DebugAssert();
        }

        private void OnEnable()
        {
            gameController.onGameStateChanged.AddListener(OnGameStateChanged);
        }

        private void OnDisable()
        {
            gameController.onGameStateChanged.RemoveListener(OnGameStateChanged);
        }

        private void OnGameStateChanged(GameController.GameStateType gameStateType)
        {
            CanPlace = gameStateType == GameController.GameStateType.InGame;
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
            if (!CanPlace) return;
            if (!HasTower) return;
            if (block is not TowerBlock) return;
            if (block.PlacedObject) return;
            var tower = gameController.GameManager.TowerSpawner.SpawnEntityType(SelectTower.GetEntityType(), block);
            block.PlacedObject = tower.gameObject;
            this.Log($"Tower placed: x:{block.X} y:{block.Y}");
        }

        private void TryRemovePlacedObject(Block block)
        {
            if (!CanPlace) return;
            if (!HasTower) return;
            if (block is not TowerBlock) return;
            if (!block.PlacedObject) return;
            gameController.GameManager.TowerSpawner.DeSpawn(block.PlacedObject.GetComponent<AbstractTower>());
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