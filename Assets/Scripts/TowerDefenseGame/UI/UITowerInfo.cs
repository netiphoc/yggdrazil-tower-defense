using TMPro;
using TowerDefenseGame.GameEntity;
using TowerDefenseGame.Spawner;
using UnityEngine;
using Utilities;

namespace TowerDefenseGame.UI
{
    public class UITowerInfo : MonoBehaviour
    {
        [Header("UI")] [SerializeField] private TextMeshProUGUI towerInfoText;

        private void Awake()
        {
            towerInfoText.gameObject.SetActive(false);
        }

        public void ShowInfo(AbstractTower tower)
        {
            towerInfoText.gameObject.SetActive(true);

            var randomDamage = tower.GetMinDamage() < tower.GetMaxDamage();
            var damageInfo =
                randomDamage ? $"{tower.GetMinDamage()}-{tower.GetMaxDamage()}" : $"{tower.GetMaxDamage()}";

            var info = $"{tower.GetInfo()}\n" +
                       $"Damage: {damageInfo}\n" +
                       $"FireRate: {tower.GetFireRate()}\n" +
                       $"Attack Range: {tower.GetFireRange()}\n" +
                       $"";
            towerInfoText.SetText(info);
        }

        public void HideInfo()
        {
            towerInfoText.gameObject.SetActive(false);
        }
    }
}