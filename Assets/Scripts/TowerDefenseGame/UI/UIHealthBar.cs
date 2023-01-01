using TMPro;
using TowerDefenseGame.GameEntity;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace TowerDefenseGame.UI
{
    public class UIHealthBar : MonoBehaviour
    {
        [SerializeField] private DamageAble damageAble;
        [SerializeField] private Image healthBarImage;
        [SerializeField] private TextMeshProUGUI healthText;

        private void Awake()
        {
            damageAble.DebugAssert();
            healthBarImage.DebugAssert();
            healthText.DebugAssert();
        }

        private void OnEnable()
        {
            damageAble.onHealthChanged.AddListener(OnHealthChanged);

            SetHealth(damageAble.GetHealth(), damageAble.GetMaxHealth());
        }

        private void OnDisable()
        {
            damageAble.onHealthChanged.RemoveListener(OnHealthChanged);
        }

        private void OnHealthChanged(float health, float maxHealth)
        {
            SetHealth(health, maxHealth);
        }

        private void SetHealth(float health, float maxHealth)
        {
            var healthScale = health / maxHealth;
            healthBarImage.fillAmount = healthScale;
            healthText.SetText($"{health:F}/{maxHealth:F}");
        }
    }
}