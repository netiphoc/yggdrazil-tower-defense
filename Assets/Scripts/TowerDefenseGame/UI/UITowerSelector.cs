using System;
using TowerDefenseGame.GameEntity.ScriptableObjects;
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

        [SerializeField] private GameManager gameManager;
        [Header("Icon")] [SerializeField] private Sprite defaultIcon;
        [SerializeField] private TowerIcon[] towerIcons;

        [Header("UI")] [SerializeField] private Transform towerIconItemContainer;
        [SerializeField] private UITowerIconItem towerIconItemTemplate;

        private void Awake()
        {
            gameManager.DebugAssert();
            towerIconItemTemplate.DebugAssert();
            defaultIcon.DebugAssert();

            towerIconItemTemplate.gameObject.SetActive(false);

            InitializeTowerIcon();
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
            foreach (var tower in gameManager.TowerPrefabSo.Towers)
            {
                var entityType = tower.GetEntityType();
                var icon = GetIcon(entityType);
                var towerIconItem = Instantiate(towerIconItemTemplate, towerIconItemContainer);
                towerIconItem.gameObject.SetActive(true);
                towerIconItem.SetIcon(icon);
            }
        }
    }
}