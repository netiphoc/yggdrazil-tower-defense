using TowerDefenseGame.GameEntity;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TowerDefenseGame.UI
{
    public class UITowerIconItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Image towerImage;
        [SerializeField] private Transform selectedBorder;

        public UnityEvent<UITowerIconItem> onTowerIconClicked;
        public UnityEvent<UITowerIconItem> onTowerIconEnter;
        public UnityEvent<UITowerIconItem> onTowerIconExit;

        public AbstractTower TowerPrefab { get; private set; }

        private void Awake()
        {
            selectedBorder.gameObject.SetActive(false);
        }

        public void SetIcon(Sprite icon)
        {
            towerImage.sprite = icon;
        }

        public void SetTowerIconIndex(AbstractTower tower)
        {
            TowerPrefab = tower;
        }

        public void SetSelected(bool selected)
        {
            selectedBorder.gameObject.SetActive(selected);
        }

        #region UI Events

        public void OnPointerClick(PointerEventData eventData)
        {
            onTowerIconClicked?.Invoke(this);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            onTowerIconEnter?.Invoke(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            onTowerIconExit?.Invoke(this);
        }

        #endregion
    }
}