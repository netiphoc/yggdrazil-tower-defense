using UnityEngine;
using UnityEngine.UI;

namespace TowerDefenseGame.UI
{
    public class UIHealthBar : MonoBehaviour
    {
        [SerializeField] private Image healthBarImage;

        public void SetHealth(float health, float maxHealth)
        {
            var healthScale = health / maxHealth;
            healthBarImage.fillAmount = healthScale;
        }
    }
}