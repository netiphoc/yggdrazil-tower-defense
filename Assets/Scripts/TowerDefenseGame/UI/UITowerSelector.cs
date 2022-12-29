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
        [Space] [SerializeField] private UITowerInfo uiTowerInfo;
        private List<UITowerIconItem> _uiTowerIconItems;

        private void Awake()
        {
            gameController.DebugAssert();
            towerPlacer.DebugAssert();
            towerIconItemTemplate.DebugAssert();
            defaultIcon.DebugAssert();
            uiTowerInfo.DebugAssert();

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
            towerIconItemContainer.gameObject.SetActive(gameStateType != GameController.GameStateType.GameOver);
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
                var towerPrefab = gameController.GameManager.TowerPrefabSo.Towers[i];
                towerIconItem.SetTowerIconIndex(towerPrefab);
                towerIconItem.onTowerIconClicked.AddListener(OnTowerIconClick);
                towerIconItem.onTowerIconEnter.AddListener(OnTowerIconEnter);
                towerIconItem.onTowerIconExit.AddListener(OnTowerIconExit);
                _uiTowerIconItems.Add(towerIconItem);
            }
        }

        private void OnTowerIconClick(UITowerIconItem towerIconItem)
        {
            towerPlacer.SelectedTower = towerIconItem.TowerPrefab;
            UpdateSelectedIcon(towerIconItem);
        }

        private void UpdateSelectedIcon(UITowerIconItem towerIconItem)
        {
            foreach (var item in _uiTowerIconItems)
                item.SetSelected(towerIconItem == item);
        }

        private void OnTowerIconEnter(UITowerIconItem towerIconItem)
        {
            uiTowerInfo.ShowInfo(towerIconItem.TowerPrefab);
        }

        private void OnTowerIconExit(UITowerIconItem towerIconItem)
        {
            uiTowerInfo.HideInfo();
        }
    }
}