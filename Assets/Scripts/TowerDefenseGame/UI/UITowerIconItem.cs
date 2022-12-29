using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TowerDefenseGame.UI
{
    public class UITowerIconItem : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image towerImage;
        [SerializeField] private Transform selectedBorder;

        public UnityEvent<UITowerIconItem> onTowerIconClicked;
        public int TowerIconIndex { get; private set; }

        private void Awake()
        {
            selectedBorder.gameObject.SetActive(false);
        }

        public void SetIcon(Sprite icon)
        {
            towerImage.sprite = icon;
        }

        public void SetTowerIconIndex(int iconIndex)
        {
            TowerIconIndex = iconIndex;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            onTowerIconClicked?.Invoke(this);
        }

        public void SetSelected(bool selected)
        {
            selectedBorder.gameObject.SetActive(selected);
        }
    }
}