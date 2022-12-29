using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utilities;

namespace TowerDefenseGame.UI
{
    public class UITowerIconItem : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image towerImage;

        public void SetIcon(Sprite icon)
        {
            towerImage.sprite = icon;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            this.Log("Clicked Icon");
        }
    }
}