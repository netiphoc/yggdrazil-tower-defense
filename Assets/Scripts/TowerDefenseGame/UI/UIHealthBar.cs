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

        private void Awake()
        {
            damageAble.DebugAssert();
        }

        private void OnEnable()
        {
            damageAble.onEntityDamaged.AddListener(OnEntityDamaged);
            SetHealth(damageAble.GetHealth(), damageAble.GetMaxHealth());
        }

        private void OnDisable()
        {
            damageAble.onEntityDamaged.RemoveListener(OnEntityDamaged);
        }

        private void OnEntityDamaged(DamageAble damageable, float damage)
        {
            SetHealth(damageAble.GetHealth(), damageAble.GetMaxHealth());
        }


        public void SetHealth(float health, float maxHealth)
        {
            var healthScale = health / maxHealth;
            healthBarImage.fillAmount = healthScale;
        }
    }
}