using TMPro;
using TowerDefenseGame.GameEntity;
using UnityEngine;
using Utilities;

namespace TowerDefenseGame.UI
{
    public class UIDamageIndicator : MonoBehaviour
    {
        [SerializeField] private DamageAble damageAble;
        [SerializeField] private TextMeshProUGUI damageText;
        [SerializeField] private float showDuration;

        private void Awake()
        {
            damageAble.DebugAssert();
            damageText.gameObject.SetActive(false);
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
            damageText.gameObject.SetActive(true);
            damageText.SetText($"-{damage:F1} HP");
            CancelInvoke(nameof(HideText));
            Invoke(nameof(HideText), showDuration);
        }

        private void HideText()
        {
            damageText.gameObject.SetActive(false);
        }
    }
}