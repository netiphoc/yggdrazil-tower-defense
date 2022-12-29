using System;
using System.Collections.Generic;
using TowerDefenseGame.GameEntity.ScriptableObjects;
using TowerDefenseGame.Spawner;
using UnityEngine;
using Utilities;

namespace TowerDefenseGame.UI
{
    public class UITowerSelector : MonoBehaviour
    {
        [Serializable]
        public struct TowerIcon : IEntityIcon
        {
            public EntityTypeSo entityType;
            public Sprite icon;
        }

        [SerializeField] private TowerPlacer towerPlacer;
        [SerializeField] private GameController gameController;
        [Header("Icon")] [SerializeField] private Sprite defaultIcon;
        [SerializeField] private TowerIcon[] towerIcons;

        [Header("UI")] [SerializeField] private Transform towerIconItemContainer;
        [SerializeField] private UITowerIconItem towerIconItemTemplate;

        private List<UITowerIconItem> _uiTowerIconItems;

        private void Awake()
        {
            gameController.DebugAssert();
            towerPlacer.DebugAssert();
            towerIconItemTemplate.DebugAssert();
            defaultIcon.DebugAssert();

            towerIconItemContainer.gameObject.SetActive(false);
            towerIconItemTemplate.gameObject.SetActive(false);

            _uiTowerIconItems = new List<UITowerIconItem>();

            InitializeTowerIcon();
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
            towerIconItemContainer.gameObject.SetActive(gameStateType != GameController.GameStateType.Prepare);
        }

        private Sprite GetIcon(EntityTypeSo entityType)
        {
            foreach (var towerdata in towerIcons)
            {
                if (towerdata.entityType != entityType) continue;
                return towerdata.icon;
            }

            return defaultIcon;
        }

        private void InitializeTowerIcon()
        {
            var towers = gameController.GameManager.TowerPrefabSo.Towers;
            for (var i = 0; i < towers.Length; i++)
            {
                var tower = towers[i];
                var entityType = tower.GetEntityType();
                var icon = GetIcon(entityType);
                var towerIconItem = Instantiate(towerIconItemTemplate, towerIconItemContainer);
                towerIconItem.gameObject.SetActive(true);
                towerIconItem.SetIcon(icon);
                towerIconItem.SetTowerIconIndex(i);
                towerIconItem.onTowerIconClicked.AddListener(OnTowerIconClick);
                _uiTowerIconItems.Add(towerIconItem);
            }
        }

        private void OnTowerIconClick(UITowerIconItem towerIconItem)
        {
            towerPlacer.SelectTower = gameController.GameManager.TowerPrefabSo.Towers[towerIconItem.TowerIconIndex];
            SetSelectedIcon(towerIconItem);
        }

        private void SetSelectedIcon(UITowerIconItem towerIconItem)
        {
            foreach (var item in _uiTowerIconItems)
            {
                item.SetSelected(towerIconItem == item);
            }
        }
    }
}