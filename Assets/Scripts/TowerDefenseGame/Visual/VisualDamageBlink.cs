using TMPro;
using TowerDefenseGame.GameEntity;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace TowerDefenseGame.Visual
{
    public class VisualDamageBlink : MonoBehaviour
    {
        [SerializeField] private DamageAble damageAble;
        [Space] [SerializeField] private float blinkDuration = 0.1f;
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private Material blinkMaterial;

        private Material _oldMaterial;

        private void Awake()
        {
            damageAble.DebugAssert();
            meshRenderer.DebugAssert();
            blinkMaterial.DebugAssert();

            _oldMaterial = meshRenderer.material;
        }

        private void OnEnable()
        {
            damageAble.onEntityDamaged.AddListener(OnEntityDamaged);
        }

        private void OnDisable()
        {
            damageAble.onEntityDamaged.RemoveListener(OnEntityDamaged);
        }

        private void OnEntityDamaged(DamageAble damageable, float damage)
        {
            SetHealth(damageAble.GetHealth(), damageAble.GetMaxHealth());
        }

        private void SetHealth(float health, float maxHealth)
        {
            DoBlink(blinkDuration);
        }

        private void DoBlink(float duration)
        {
            meshRenderer.material = blinkMaterial;
            CancelInvoke(nameof(StopBlink));
            Invoke(nameof(StopBlink), duration);
        }

        private void StopBlink()
        {
            meshRenderer.material = _oldMaterial;
        }
    }
}